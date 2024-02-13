namespace MinimalApi_test____Dapper___PostgreSQL.Interfaces
{
    public interface ICacheRedisService
    {
       public T GetData<T>(string key);
        public bool SetData<T>(string key, T value, TimeSpan expirationTime );
        public object DeleteData<T>(string key, T value);
    }
}
