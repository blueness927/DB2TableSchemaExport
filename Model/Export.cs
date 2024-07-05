using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2SchemaExport.Model
{
    public class Export
    {
        public class ExportSchema
        {
            public string SchemaName { get; set; }

            public string TableName { get; set; }
            public string Description { get; set; }
        }

        public class ExportTable
        {
            public string No { get; set; }
            public string TableName { get; set; }
            public string ColumnName { get; set; }
            public string DataType { get; set; }
            public string PkConstraint { get; set; }



            public string Identity { get; set; }
            public string Nullable { get; set; }

            public string Length { get; set; }
            public string Description { get; set; }

        }
    }
}
