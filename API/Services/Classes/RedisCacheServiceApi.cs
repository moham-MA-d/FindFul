using API.Services.Interfaces;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace API.Services.Classes
{
    public class RedisCacheServiceApi : IRedisCacheServiceApi
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheServiceApi(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }


        public async Task<string> GetCachedResponseAsync(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringGetAsync(key);
        }


        public async Task SetCacheResponseAsync(string key, string value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(key, value);
        }
    }
}
