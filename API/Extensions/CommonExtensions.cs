using System;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class CommonExtensions
    {

        public static string GetCurrentLocationUri(this HttpContext httpContext)
        {
            var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host.ToUriComponent()}{httpContext.Request.Path}";
            var index = baseUrl.LastIndexOf("/", StringComparison.Ordinal);
            if (index >= 0)
                baseUrl = baseUrl[..index]; // or index + 1 to keep slash




            return baseUrl;
        }
    }
}
