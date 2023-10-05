using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Soccer.Models;

namespace Soccer.Utils
{
    public class DBConnUtil
    {
        private IConfiguration _configuration;

        public DBConnUtil(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("master"));
        }

        public List<T> QueryAll<T>(string sp)
        {
            List<T> results;
            using (var connection = GetConnection())
            {
                results = connection.Query<T>(sp, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return results;
        }

        public void UpdateAll(string sp, object parameterObject)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(sp, param: parameterObject, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
