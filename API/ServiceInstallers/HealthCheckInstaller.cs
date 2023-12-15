using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.ServiceInstallers;

public class HealthCheckInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        //TODO Commented for deployment
        services.AddHealthChecks().AddDbContextCheck<DataContext>()

        //needs docker
        //.AddCheck<RedisHealthCheck>("Redis")
        ;
    }
}
