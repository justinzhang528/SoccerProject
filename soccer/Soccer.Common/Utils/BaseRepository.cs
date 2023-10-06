using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Soccer.Common.Utils
{
    public class BaseRepository
    {
        private IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("matchResultDb"));
        }

        public void UpdateAll(string sp, object parameterObject)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(sp, param: parameterObject, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public List<T> Query<T>(string sp, DynamicParameters? parameters = null)
        {
            List<T> results;
            using (var connection = GetConnection())
            {
                results = connection.Query<T>(sp, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return results;
        }
    }
}
