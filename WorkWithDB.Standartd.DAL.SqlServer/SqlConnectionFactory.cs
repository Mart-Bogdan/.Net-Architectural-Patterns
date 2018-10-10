using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace WorkWithDB.Standartd.DAL.SqlServer
{
    static public class SqlConnectionFactory
    {
        public static  SqlConnection CreateConnection(string defaultConnection)
        {
            var connectionString = defaultConnection;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
}
