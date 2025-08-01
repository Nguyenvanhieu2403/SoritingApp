using System.Data;
using Microsoft.Data.SqlClient;

namespace SortingApp_Desktop.Common.@base
{
    public class DbConnectionFactory
    {
        public static IDbConnection GetDbConnection(string connectionString)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
