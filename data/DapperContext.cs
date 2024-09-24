using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MyConsoleApp.Data
{
    public class DapperContext
    {
        private readonly string _connectionString = "Server=localhost,1433;Database=TestTable;Trusted_connection=false;TrustServerCertificate=True;User Id=sa;Password=YourStrong!Passw0rd;";

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            return connection.Query<T>(sql);
        }

        public bool ExecuteSql(string sql, object? parameters = null)
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            return connection.Execute(sql, parameters) > 0;
        }
    }
}