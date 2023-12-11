using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace API.ServiceInstallers
{
    public class LogInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var serilog = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.Console()
                    .WriteTo.File("logs/findfulLog-.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();

            services.AddSerilog(serilog);
        }
    }
}
