using API.Helpers;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Middlewares
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSecounds;

        public CachedAttribute(int timeToLiveSecounds)
        {
            _timeToLiveSecounds = timeToLiveSecounds;
        }
        public int TimeToLiveSecounds => _timeToLiveSecounds;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // before the controller :
            // check if the request is cached
            // return true else go to next()

            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();
            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }

            var cacheSerice = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheServiceApi>();


            // we need to identify each is unique and the way to that is cacheKey
            // we get actual url and query string and use them as a cacheKey
            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            var cacheResponse = await cacheSerice.GetCachedResponseAsync(cacheKey);

            // a response from redis:
            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                context.Result = contentResult;
                return;
            }


            var executedContext = await next();

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheSerice.SetCacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSecounds));
            }

            // after the controller :
            // cache the response
        }


        private static string GenerateCacheKey(HttpRequest httpRequest)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{httpRequest.Path}");

            foreach (var (key, value) in httpRequest.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
