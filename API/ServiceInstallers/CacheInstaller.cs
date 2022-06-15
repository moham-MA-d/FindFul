using API.Helpers;
using API.Helpers.HealthChecks;
using API.Services.Classes;
using API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace API.ServiceInstallers
{
    public class CacheInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //TODO Commented for deployment
            //var redisCacheSettings = new RedisCacheSettings();
            //configuration.GetSection(nameof(redisCacheSettings)).Bind(redisCacheSettings);
            //services.AddSingleton(redisCacheSettings);

            //if (!redisCacheSettings.Enabled) return;

            //var redisConnectionString = configuration.GetValue<string>("ConnectionStrings:RedisConnection");

            //var option = new ConfigurationOptions
            //{
            //    AbortOnConnectFail = true,
            //    ServiceName = redisConnectionString
            //};
            


            //needs docker
            //services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(redisConnectionString));
            //services.AddSingleton<IResponseCacheServiceApi, ResponseCacheServiceApi>();
            //services.AddSingleton<IRedisCacheServiceApi, RedisCacheServiceApi>();
            //services.AddHostedService<RedisSubscriber>();



            //var redis = ConnectionMultiplexer.Connect("172.17.0.2");
            //services.AddScoped(d => redis.GetDatabase());
        }
    }
}
