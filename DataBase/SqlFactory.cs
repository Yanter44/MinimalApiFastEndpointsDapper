using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;

namespace MinimalApi_test____Dapper___PostgreSQL.DataBase
{
    public class SqlFactory : IDbConnectionFactory
    {     
        public SqlFactory()
        {
         
        }
        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var connection = new SqlConnection();
            connection.ConnectionString = "Server=DESKTOP-S9AIDDH\\SQLEXPRESS; Database=FluentMigratorDb; Trusted_Connection=True; TrustServerCertificate=True";
            await connection.OpenAsync();
            return connection;
        }

    }
}
