using System.Data;

namespace MinimalApi_test____Dapper___PostgreSQL.DataBase
{
    public interface IDbConnectionFactory
    {
        public Task<IDbConnection> CreateConnectionAsync();
    }
}
