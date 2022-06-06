using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IRedisCacheServiceApi
    {
        Task<string> GetCachedResponseAsync(string key);
        Task SetCacheResponseAsync(string key, string value);
    }
}
