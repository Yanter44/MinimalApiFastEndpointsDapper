using MinimalApi_test____Dapper___PostgreSQL.Interfaces;
using Namotion.Reflection;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text.Json.Serialization;

namespace MinimalApi_test____Dapper___PostgreSQL.Services
{
    public class RedisCacheService : ICacheRedisService
    {
        private readonly IDatabase _cachebase;
        public  RedisCacheService(IDatabase cachebase)
        {
            _cachebase = cachebase;
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _cachebase = redis.GetDatabase();
        }
        public T GetData<T>(string key)
        {
            var value = _cachebase.StringGet(key);
            if(!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            else
            {
                return default;
            }
        }

        public bool SetData<T>(string key, T value, TimeSpan timeSpan)
        {
            var time = timeSpan;
            var goceched = _cachebase.StringSet(key, JsonConvert.SerializeObject(value), timeSpan);
            return goceched;
            
        }

        public object DeleteData<T>(string key, T value)
        {
            var exist = _cachebase.KeyExists(key);
            if(exist) 
            {
               return _cachebase.KeyDelete(key);
            }
            return false;
        }

    }
}
