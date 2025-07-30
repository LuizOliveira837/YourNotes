using Dapper;
using Microsoft.Data.SqlClient;

namespace YourNotes.Persistence.Data.Migrations
{
    public static class MigrationDatabase
    {

        public static void EnsureDatabase(string name)
        {
            var parameters = new DynamicParameters();

            var connectionString = new SqlConnectionStringBuilder(name);

            var database = connectionString.InitialCatalog;

            connectionString.InitialCatalog = "";

            parameters.Add("name", database);


            using var connection = new SqlConnection(connectionString.ConnectionString);
            var records = connection.Query("SELECT 1 FROM sys.databases WHERE name = @name",
                 parameters);
            if (!records.Any())
            {
                connection.Execute($"CREATE DATABASE {database}");
            }

        }
    }
}
