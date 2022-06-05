using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace API.Middlewares
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] 
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {

        //before controller executed: 

        public const string ApiKeyValue = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //var getApiKeyFromQueryStringExample = context.HttpContext.Request.Query[ApiKeyValue];

            //get ApiKey from header  
            if (context.HttpContext.Request.Headers.TryGetValue(ApiKeyValue, out var potentialApiKey))
            {
                context.Result = new UnauthorizedResult(); //401
                return;
            }

            //Get actual api key, we have to validate `potentialApiKey`
            //Get the configuration from DI Container 
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("ApiKey");

            if (!apiKey.Equals(potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            //here controller will execute:
            await next();


            //after  controller executed: 
        }
    }
}
