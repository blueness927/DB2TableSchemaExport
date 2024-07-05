using DB2SchemaExport.Model;
using IBM.Data.Db2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2SchemaExport
{
    public class Connection
    {
        private Setting _info;
        public Connection(Setting info)
        {
            _info = info;
        }
        public DB2Connection GetDB2Connection()
        {
            try
            {
                string conStr = $"Server={_info.SERVER}:{_info.PORT};Database={_info.DB};UID={_info.UID};PWD={_info.PWD}";
                var conn = new DB2Connection(conStr);
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
