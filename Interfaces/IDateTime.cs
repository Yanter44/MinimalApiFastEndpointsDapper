namespace MinimalApi_test____Dapper___PostgreSQL.Interfaces
{
    public interface IDateTime
    {
        public Task<DateTime> GetTimeNow();
    }
}
