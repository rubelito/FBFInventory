using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace FBFInventory.Infrastructure.EntityFramework
{
    public class EfSqlServer : IDatabaseType
    {
        private readonly string _connectionName;

        public EfSqlServer(string connectionName)
        {
            _connectionName = connectionName;
        }

        public DbConnection Connectionstring()
        {
            return new SqlConnection(ConstrucConnectionString());
        }

        private string ConstrucConnectionString()
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[_connectionName];
            var strConnection = connectionStringSettings.ConnectionString;
            var builder = new SqlConnectionStringBuilder(strConnection);

            return builder.ConnectionString;
        } 
    }
}