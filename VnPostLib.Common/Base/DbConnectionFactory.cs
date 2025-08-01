using System.Data;
using Microsoft.Data.SqlClient;

namespace VnPostLib.Common.Base
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
