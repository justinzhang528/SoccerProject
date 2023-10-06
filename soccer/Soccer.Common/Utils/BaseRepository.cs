using System.Data;
using System.Reflection;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Soccer.Common.Utils
{
    public class BaseRepository
    {
        private readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("matchResultDb"));
        }

        protected void UpdateAll(string sp, object parameterObject)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(sp, param: parameterObject, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        protected List<T> Query<T>(string sp, DynamicParameters? parameters = null)
        {
            List<T> results;
            using (var connection = GetConnection())
            {
                results = connection.Query<T>(sp, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return results;
        }

        protected DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(EnumCondition))
                    dataTable.Columns.Add(prop.Name, typeof(Int32));
                else
                    dataTable.Columns.Add(prop.Name,
                        Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
