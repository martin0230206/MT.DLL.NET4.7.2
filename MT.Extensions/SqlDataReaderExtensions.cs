using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static int GetInt32(this SqlDataReader reader, string columnName) =>
            reader.GetInt32(reader.GetOrdinal(columnName));

        public static string GetString(this SqlDataReader reader, string columnName) =>
            reader.GetString(reader.GetOrdinal(columnName));
    }
}
