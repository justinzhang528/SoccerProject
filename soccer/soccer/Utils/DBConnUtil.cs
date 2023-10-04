using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
    }
}
