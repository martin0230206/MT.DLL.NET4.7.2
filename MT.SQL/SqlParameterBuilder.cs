using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.SQL
{
    public class SqlParameterBuilder
    {
        private List<SqlParameter> _sqlParameters = new List<SqlParameter>();

        public SqlParameterBuilder()
        {
            _sqlParameters = new List<SqlParameter>();
        }

        public SqlParameterBuilder AddInt(string paramName, int value)
        {
            _sqlParameters.Add(new SqlParameter(paramName, SqlDbType.Int) { Value = value });
            return this;
        }

        public SqlParameterBuilder AddInt_N(string paramName, int? value)
        {
            if (value.HasValue)
                AddInt(paramName, value.Value);

            _sqlParameters.Add(new SqlParameter(paramName, SqlDbType.Int) { Value = DBNull.Value });
            return this;
        }

        public SqlParameterBuilder AddDateTime(string paramName, DateTime value)
        {
            _sqlParameters.Add(new SqlParameter(paramName, SqlDbType.DateTime) { Value = value });
            return this;
        }

        public SqlParameterBuilder AddDateTime_N(string paramName, DateTime? value)
        {
            if (value.HasValue)
                AddDateTime(paramName, value.Value);

            _sqlParameters.Add(
                new SqlParameter(paramName, SqlDbType.DateTime) { Value = DBNull.Value }
            );
            return this;
        }

        public SqlParameterBuilder AddBool(string paramName, bool value)
        {
            _sqlParameters.Add(new SqlParameter(paramName, SqlDbType.Binary) { Value = value });
            return this;
        }

        public SqlParameterBuilder AddBool_N(string paramName, bool? value)
        {
            if (value.HasValue)
                AddBool(paramName, value.Value);

            _sqlParameters.Add(new SqlParameter(paramName, SqlDbType.Binary) { Value = value });
            return this;
        }

        public SqlParameterBuilder AddNVarchar(string paramName, int length, string value)
        {
            _sqlParameters.Add(
                new SqlParameter(paramName, SqlDbType.NVarChar, length) { Value = value }
            );
            return this;
        }

        public SqlParameter[] Build() => _sqlParameters.ToArray();
    }
}
