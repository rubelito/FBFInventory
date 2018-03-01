using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace FBFInventory.Infrastructure.EntityFramework
{
    public class EfSqlServer : IDatabaseType
    {
        private readonly string _connectionName;
        private readonly string _ipAddress;

        public EfSqlServer(string connectionName, string ipAddress){
            _connectionName = connectionName;
            _ipAddress = ipAddress;
        }

        public DbConnection Connectionstring(){
            return new SqlConnection(ConstrucConnectionString());
        }

        private string ConstrucConnectionString(){
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[_connectionName];
            var strConnection = connectionStringSettings.ConnectionString;
            strConnection = ReplaceIPAddress(strConnection);
            var builder = new SqlConnectionStringBuilder(strConnection);

            return builder.ConnectionString;
        }

        private string ReplaceIPAddress(string connectionString){
            return connectionString.Replace("127.0.0.1", _ipAddress);
        }
    }
}