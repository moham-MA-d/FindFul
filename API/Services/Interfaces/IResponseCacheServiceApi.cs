using System;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IResponseCacheServiceApi
    {
        Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}
