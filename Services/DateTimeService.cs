using MinimalApi_test____Dapper___PostgreSQL.DataBase;
using MinimalApi_test____Dapper___PostgreSQL.Interfaces;

namespace MinimalApi_test____Dapper___PostgreSQL.Services
{
    public class DateTimeService : IDateTime
    {
        public async Task<DateTime> GetTimeNow()
        {
            var data = DateTime.Now;
            return data;
        }
    }
}
