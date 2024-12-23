using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.SQL
{
    public class SqlConnectionHelper
    {
        public String GetConnectionString(string key)
        {
            return System
                .Configuration
                .ConfigurationManager
                .ConnectionStrings[key]
                .ConnectionString;
        }

        public SqlConnection GetSqlconn(string key)
        {
            return new SqlConnection(GetConnectionString(key));
        }
    }
}
