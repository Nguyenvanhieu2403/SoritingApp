using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext
{
    public class ConnectToSql
    {
        private readonly IConfiguration _configuration;

        public ConnectToSql(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnectDB()
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            return connectionString;
        }

        public IDbConnection CreateConnection() => new Microsoft.Data.SqlClient.SqlConnection(GetConnectDB());

    }
}
