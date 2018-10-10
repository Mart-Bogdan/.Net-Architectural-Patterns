using System.Configuration;
using System.Data.SqlClient;

static internal class SqlConnectionFactory
{
    public static SqlConnection CreateConnection()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        var connection = new SqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}