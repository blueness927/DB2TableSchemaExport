using Dapper;
using DB2SchemaExport.Model;
using NPOI.HPSF;
using NPOI.XSSF.UserModel;
using System;
using System.Linq;
using System.Xml.Linq;

namespace DB2SchemaExport
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private const string templatePath = @"Template\template.xlsx";

        private const string outputDocPath = @"Export\";
        private string server;
        private string db;
        private string uid;
        private string pwd;
        private string port;
        public Setting info;

        private bool checkTbx()
        {
            var tbxs = this.Controls.OfType<TextBox>().ToList();

            foreach (var tbx in tbxs)
            {
                if (string.IsNullOrEmpty(tbx.Text))
                {
                    MessageBox.Show("Input field must not be empty");
                    return false;
                }
            }
            return true;
        }

        private void btn_Connection_Click(object sender, EventArgs e)
        {
            var chkResult = checkTbx();
            if (!chkResult) return;

            server = tbx_server.Text.Trim().ToLower();
            db = tbx_db.Text.Trim().ToLower();
            uid = tbx_username.Text.Trim().ToLower();
            pwd = tbx_pwd.Text.Trim().ToLower();
            port = tbx_port.Text.Trim().ToLower();
            info = new Setting
            {
                SERVER = server,
                DB = db,
                UID = uid,
                PWD = pwd,
                PORT = port,
                SCHEMA = ""
            };

            var con = new Connection(info);

            try
            {
                var sql = $"SELECT DISTINCT TABSCHEMA FROM SYSCAT.TABLES WHERE OWNER=upper('{info.UID}')";
                using (var db = con.GetDB2Connection())
                {
                    var result = db.Query<string>(sql).ToList();

                    foreach (var item in result)
                    {
                        cbx_schema.Items.Add(item);
                    }

                }

                MessageBox.Show("OK");
                cbx_schema.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                cbx_schema.DataSource = null;
                MessageBox.Show(exception.Message);

            }
        }

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All.Checked)
            {
                chk_Cancel.Checked = false;
                chk_All.Checked = true;

            }
            if (chkList_Table.Items.Count != 0)
            {
                for (int i = 0; i < chkList_Table.Items.Count; i++)
                    chkList_Table.SetItemChecked(i, chk_All.Checked);
            }

        }

        private void chk_Cancel_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Cancel.Checked)
            {
                chk_All.Checked = false;
                chk_Cancel.Checked = true;
            }
            if (chkList_Table.Items.Count != 0)
            {
                for (int i = 0; i < chkList_Table.Items.Count; i++)
                    chkList_Table.SetItemChecked(i, !chk_Cancel.Checked);
            }

        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            btn_export.Enabled = false;
            btn_Connection.Enabled = false;
            
            
            if (string.IsNullOrEmpty(cbx_schema.Text))
            {
                btn_export.Enabled = true;
                btn_Connection.Enabled = true;
                
                MessageBox.Show("not connected yet");
                return;
            }
            
            if (chkList_Table.Items.Count != 0&& chkList_Table.CheckedItems.Count!=0)
            {
                var checkedTables = new List<string>();
                foreach (var table in chkList_Table.CheckedItems)
                {
                    checkedTables.Add(table.ToString());
                }

                var con = new Connection(info);
                var excel = new ExportExcel(checkedTables, info.SCHEMA, con);
                excel.GenerateDocument();
                btn_export.Enabled = true;
                btn_Connection.Enabled = true;
            }
            else
            {
                MessageBox.Show("please choose table");
            }
        }
        public class ExportExcel
        {
            public List<string> _tables;
            public string _schema;
            private Connection _con;
            public ExportExcel(List<string> tables, string schema, Connection con)
            {
                _tables = tables;
                _schema = schema;
                _con = con;
            }
            public List<Export.ExportTable> GetTables(string table, string schema)
            {
                var sql = $"SELECT TABNAME AS tablename, COLNO AS No ,COLNAME AS ColumnName, TYPENAME AS DataType, LENGTH AS Length ,SCALE AS NumericScale,IDENTITY AS Identity, REMARKS AS Description ,NULLS AS Nullable,KEYSEQ AS PkConstraint,DEFAULT AS Default FROM syscat.columns WHERE TABNAME='{table}' and " +
                    $"TABSCHEMA='{schema}' order by colno asc";
                using (var db = _con.GetDB2Connection())
                {
                    var result = db.Query<Export.ExportTable>(sql).ToList();
                    return result;
                }
            }

            public List<Export.ExportSchema> GetSchema(string schema, List<string> tables)
            {
                var arys = tables.ToArray();
                string tablenames = string.Join("','", arys);
                var sql = $"select TABSCHEMA as SchemaName ,TABNAME as TableName , REMARKS as Description from syscat.tables WHERE TABSCHEMA in'{_schema}' AND TABNAME IN ('{tablenames}');";
                using (var db = _con.GetDB2Connection())
                {
                    var result = db.Query<Export.ExportSchema>(sql).ToList();
                    return result;
                }
            }
            public void GenerateDocument()
            {

                var schemaData = GetSchema(_schema, _tables);
                const string sheetName4TableListTempalte = "#TableListTemplate";
                const string sheetName4TableSchemaTempalte = "#TableSchemaTemplate";
                const string tagName4TableNo = "#table.no";
                const string tagName4TableName = "#table.name";
                const string tagName4TableDesc = "#table.description";
                const string tagName4TableType = "#table.type";
                const string tagName4ColumnNo = "#column.no";
                const string tagName4ColumnName = "#column.name";
                const string tagName4ColumnPK = "#column.pk";
                const string tagName4ColumnNullable = "#column.nullable";
                const string tagName4ColumnIdentity = "#column.identity";
                const string tagName4ColumnDataType = "#column.datatype";
                const string tagName4ColumnDesc = "#column.description";
                const string tagName4ColumnLength = "#column.length";
                XSSFWorkbook workbook;
                using (FileStream fs = new FileStream(templatePath, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(fs);
                }


                // TABLE LIST

                // clone tempalte table list sheet & rename sheet
                var tableSheet = workbook.CloneSheet(workbook.GetSheetIndex(sheetName4TableListTempalte));
                workbook.SetSheetName(workbook.NumberOfSheets - 1, "Table List"); 

                var tableIndex = 1;
                foreach (var table in schemaData)
                {
                    var noLocation = tableSheet.FindCellLocation(tagName4TableNo);
                    if (noLocation.HasValue)
                    {
                        tableSheet.CopyRow(noLocation.Value.Y, noLocation.Value.Y + 1);
                        var newRowIndex = noLocation.Value.Y;
                        tableSheet.SetFirstMatchCellContentInRow(newRowIndex, tagName4TableNo, tableIndex.ToString());
                        tableSheet.SetFirstMatchCellHyperlinkInRow(newRowIndex, tagName4TableName,
                            table.TableName, tableIndex.ToString());
                        tableSheet.SetFirstMatchCellContentInRow(newRowIndex, tagName4TableDesc,
                            table.Description);

                        tableIndex++;

                    }

                    if (schemaData.Count == tableIndex - 1)
                    {
                        tableSheet.RemoveFirstMatchRow(tagName4TableNo);
                    }
                }

                // TABLE SCHEMA SHEETS

                tableIndex = 1;
                foreach (var _table in schemaData)
                {
                    
                    // clone tempalte schema sheet & rename sheet
                    var schemaSheet = workbook.CloneSheet(workbook.GetSheetIndex(sheetName4TableSchemaTempalte));
                    workbook.SetSheetName(workbook.NumberOfSheets - 1, tableIndex.ToString());

                    var table = GetTables(_table.TableName, _table.SchemaName);

                    // table name & description
                    schemaSheet.SetFirstMatchCellContent(tagName4TableName, _table.TableName);
                    schemaSheet.SetFirstMatchCellContent(tagName4TableDesc, _table.Description);
                    
               
                    int count= 0;
                    // prepare each column info
                    foreach (var column in table)
                    {
                        count++;
                        var noLocation = schemaSheet.FindCellLocation(tagName4ColumnNo);

                        if (noLocation.HasValue)
                        {
                            // copy the column info row from tempalte
                            schemaSheet.CopyRow(noLocation.Value.Y, noLocation.Value.Y + 1);
                            string colNo = (int.Parse(column.No) + 1).ToString();
                            var newRowIndex = noLocation.Value.Y;
                            schemaSheet.SetFirstMatchCellContentInRow(newRowIndex, tagName4ColumnNo, colNo);
                            schemaSheet.SetFirstMatchCellContentInRow(newRowIndex, tagName4ColumnName, column.ColumnName);
                            schemaSheet.SetFirstMatchCellContentInRow(newRowIndex, tagName4ColumnPK, column.PkConstraint == "1" ? "V" : "");
                            schemaSheet.SetFirstMatchCellContentInRow(newRowIndex, tagName4ColumnLength, column.Length);
                            schemaSheet.SetFirstMatchCellContentInRow(newRowIndex, tagName4ColumnNullable, column.Nullable == "Y" ? "V" : "");
                            schemaSheet.SetFirstMatchCellContentInRow(newRowIndex, tagName4ColumnIdentity, column.Identity == "Y" ? "V" : "");
                            schemaSheet.SetFirstMatchCellContentInRow(newRowIndex, tagName4ColumnDataType, string.IsNullOrWhiteSpace(column.DataType) ? "" : column.DataType);

                            schemaSheet.SetFirstMatchCellContentInRow(newRowIndex, tagName4ColumnDesc, string.IsNullOrWhiteSpace(column.Description) ? "" : column.Description);
                        }

                        if (table.Count == count)
                        {
                            schemaSheet.RemoveFirstMatchRow(tagName4ColumnNo);
                        }
                    }
                    tableIndex++;

                }

                // remove template sheet
                workbook.RemoveSheetAt(workbook.GetSheetIndex(sheetName4TableListTempalte));
                workbook.RemoveSheetAt(workbook.GetSheetIndex(sheetName4TableSchemaTempalte));

                // save as another excel file
                var filename = schemaData.First().SchemaName + "_TableSchema_"+DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".xlsx";
                var outpath = outputDocPath + filename;
                Directory.CreateDirectory(Path.GetDirectoryName(outpath));
                try
                {
                    using (FileStream stream = new FileStream(outpath, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(stream);
                        MessageBox.Show("The file has been exported to " + outpath);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // set null 
                workbook = null;
            }

        }

        private void cbx_schema_SelectedIndexChanged(object sender, EventArgs e)
        {
            var con = new Connection(info);
            info.SCHEMA = cbx_schema.SelectedItem.ToString();
            var sql = $"SELECT TABNAME FROM SYSCAT.TABLES WHERE TABSCHEMA=upper('{info.SCHEMA}')";
            using (var db = con.GetDB2Connection())
            {
                var result = db.Query<string>(sql).ToList();
                foreach (var item in result)
                {
                    chkList_Table.Items.Add(item);
                }
            }
        }

        private void chk_All_Click(object sender, EventArgs e)
        {

        }
    }
}
