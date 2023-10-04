using Microsoft.Data.SqlClient;

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
