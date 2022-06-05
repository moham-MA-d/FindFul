using API.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace API.Services.Classes
{
    public class ResponseCacheServiceApi : IResponseCacheServiceApi
    {
        private readonly IDistributedCache _dsitributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;


        public ResponseCacheServiceApi(IDistributedCache dsitributedCache, IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _dsitributedCache = dsitributedCache;
        }

        public async Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null) return;
            
            var serializedResponse = JsonConvert.SerializeObject(response);
            await _dsitributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive,
            });
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _dsitributedCache.GetStringAsync(cacheKey);
            return String.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
        }
    }
}
