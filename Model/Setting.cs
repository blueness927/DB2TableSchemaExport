using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2SchemaExport.Model
{
    public class Setting
    {
        public string SERVER { get; set; }
        public string DB { get; set; }

        public string UID { get; set; }

        public string PWD { get; set; }

        public string PORT { get; set; }

        public string SCHEMA { get; set; }
    }
}
