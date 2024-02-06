using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;

namespace MinimalApi_test____Dapper___PostgreSQL.DataBase
{
    public class SqlFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

    }
}
