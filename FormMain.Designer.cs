namespace DB2SchemaExport
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            tbx_server = new TextBox();
            tbx_port = new TextBox();
            tbx_username = new TextBox();
            tbx_pwd = new TextBox();
            label5 = new Label();
            label6 = new Label();
            btn_Connection = new Button();
            btn_export = new Button();
            chk_All = new CheckBox();
            chk_Cancel = new CheckBox();
            label7 = new Label();
            cbx_schema = new ComboBox();
            tbx_db = new TextBox();
            chkList_Table = new CheckedListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 46);
            label1.Name = "label1";
            label1.Size = new Size(108, 23);
            label1.TabIndex = 0;
            label1.Text = "Server Host";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(89, 113);
            label2.Name = "label2";
            label2.Size = new Size(46, 23);
            label2.TabIndex = 1;
            label2.Text = "Port";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(353, 109);
            label3.Name = "label3";
            label3.Size = new Size(90, 23);
            label3.TabIndex = 3;
            label3.Text = "Password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(347, 46);
            label4.Name = "label4";
            label4.Size = new Size(96, 23);
            label4.TabIndex = 2;
            label4.Text = "Username";
            // 
            // tbx_server
            // 
            tbx_server.Location = new Point(141, 39);
            tbx_server.Name = "tbx_server";
            tbx_server.Size = new Size(182, 30);
            tbx_server.TabIndex = 4;
            // 
            // tbx_port
            // 
            tbx_port.Location = new Point(141, 102);
            tbx_port.Name = "tbx_port";
            tbx_port.Size = new Size(182, 30);
            tbx_port.TabIndex = 5;
            // 
            // tbx_username
            // 
            tbx_username.Location = new Point(449, 39);
            tbx_username.Name = "tbx_username";
            tbx_username.Size = new Size(182, 30);
            tbx_username.TabIndex = 6;
            // 
            // tbx_pwd
            // 
            tbx_pwd.Location = new Point(449, 106);
            tbx_pwd.Name = "tbx_pwd";
            tbx_pwd.Size = new Size(182, 30);
            tbx_pwd.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(37, 175);
            label5.Name = "label5";
            label5.Size = new Size(90, 23);
            label5.TabIndex = 8;
            label5.Text = "Database";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(71, 298);
            label6.Name = "label6";
            label6.Size = new Size(56, 23);
            label6.TabIndex = 9;
            label6.Text = "Table";
            // 
            // btn_Connection
            // 
            btn_Connection.Location = new Point(449, 169);
            btn_Connection.Name = "btn_Connection";
            btn_Connection.Size = new Size(182, 34);
            btn_Connection.TabIndex = 10;
            btn_Connection.Text = "Connection";
            btn_Connection.UseVisualStyleBackColor = true;
            btn_Connection.Click += btn_Connection_Click;
            // 
            // btn_export
            // 
            btn_export.Location = new Point(449, 235);
            btn_export.Name = "btn_export";
            btn_export.Size = new Size(182, 34);
            btn_export.TabIndex = 11;
            btn_export.Text = "Export";
            btn_export.UseVisualStyleBackColor = true;
            btn_export.Click += btn_export_Click;
            // 
            // chk_All
            // 
            chk_All.AutoSize = true;
            chk_All.Location = new Point(141, 297);
            chk_All.Name = "chk_All";
            chk_All.Size = new Size(58, 27);
            chk_All.TabIndex = 13;
            chk_All.Text = "All";
            chk_All.UseVisualStyleBackColor = true;
            chk_All.CheckedChanged += chk_All_CheckedChanged;
            chk_All.Click += chk_All_Click;
            // 
            // chk_Cancel
            // 
            chk_Cancel.AutoSize = true;
            chk_Cancel.Location = new Point(230, 297);
            chk_Cancel.Name = "chk_Cancel";
            chk_Cancel.Size = new Size(93, 27);
            chk_Cancel.TabIndex = 14;
            chk_Cancel.Text = "Cancel";
            chk_Cancel.UseVisualStyleBackColor = true;
            chk_Cancel.CheckedChanged += chk_Cancel_CheckedChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(50, 241);
            label7.Name = "label7";
            label7.Size = new Size(77, 23);
            label7.TabIndex = 15;
            label7.Text = "Schema";
            // 
            // cbx_schema
            // 
            cbx_schema.FormattingEnabled = true;
            cbx_schema.Location = new Point(141, 233);
            cbx_schema.Name = "cbx_schema";
            cbx_schema.Size = new Size(182, 31);
            cbx_schema.TabIndex = 16;
            cbx_schema.SelectedIndexChanged += cbx_schema_SelectedIndexChanged;
            // 
            // tbx_db
            // 
            tbx_db.Location = new Point(141, 168);
            tbx_db.Name = "tbx_db";
            tbx_db.Size = new Size(182, 30);
            tbx_db.TabIndex = 17;
            // 
            // chkList_Table
            // 
            chkList_Table.FormattingEnabled = true;
            chkList_Table.Location = new Point(79, 368);
            chkList_Table.Name = "chkList_Table";
            chkList_Table.Size = new Size(544, 220);
            chkList_Table.TabIndex = 19;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(705, 624);
            Controls.Add(chkList_Table);
            Controls.Add(tbx_db);
            Controls.Add(cbx_schema);
            Controls.Add(label7);
            Controls.Add(chk_Cancel);
            Controls.Add(chk_All);
            Controls.Add(btn_export);
            Controls.Add(btn_Connection);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(tbx_pwd);
            Controls.Add(tbx_username);
            Controls.Add(tbx_port);
            Controls.Add(tbx_server);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormMain";
            Text = "DB2SchemaExport";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox tbx_server;
        private TextBox tbx_port;
        private TextBox tbx_username;
        private TextBox tbx_pwd;
        private Label label5;
        private Label label6;
        private Button btn_Connection;
        private Button btn_export;
        private CheckBox chk_All;
        private CheckBox chk_Cancel;
        private Label label7;
        private ComboBox cbx_schema;
        private TextBox tbx_db;
        private Label label8;
        private CheckedListBox chkList_Table;
    }
}
