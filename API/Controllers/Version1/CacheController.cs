using API.Controllers.Version1.Base;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.Version1
{
    public class CacheController : BaseApiController
    {
        private readonly IRedisCacheServiceApi _redisCacheServiceApi;

        public CacheController(IRedisCacheServiceApi redisCacheServiceApi)
        {
            _redisCacheServiceApi = redisCacheServiceApi;
        }


        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] string key)
        {
            var r = await _redisCacheServiceApi.GetCachedResponseAsync(key);
            return string.IsNullOrEmpty(r) ? NotFound() : Ok(r);
        }


        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromBody] KeyValuePair<string, string> keyValue)
        {
            await _redisCacheServiceApi.SetCacheResponseAsync(keyValue.Key, keyValue.Value);
            return Ok();
        }
    }
}
