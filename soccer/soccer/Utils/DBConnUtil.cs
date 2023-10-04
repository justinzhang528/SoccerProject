using Microsoft.Data.SqlClient;

namespace Soccer.Utils
{
    public class DBConnUtil
    {
        public SqlConnection GetConnection()
        {
            string connectionString = "Server=localhost;Database=master;User Id=sa;Password=Weijun528@;Trusted_Connection=True;Integrated Security=SSPI;TrustServerCertificate=True";
            return new SqlConnection(connectionString);
        }
    }
}
