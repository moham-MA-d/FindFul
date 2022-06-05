using API.Helpers;
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
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(redisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);

            if (!redisCacheSettings.Enabled) return;

            services.AddStackExchangeRedisCache(opt => opt.Configuration = redisCacheSettings.ConnectionString);
            services.AddSingleton<IResponseCacheServiceApi, ResponseCacheServiceApi>();


            var redisConnectionString = configuration.GetValue<string>("RedisConnection");
            services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(redisConnectionString));
        }
    }
}
