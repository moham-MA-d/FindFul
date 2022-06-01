using System;
using System.Linq;
using API.ServiceInstallers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var a = typeof(Startup);
            var installers = typeof(Startup).Assembly.ExportedTypes
                .Where(x => typeof(IServiceInstaller).IsAssignableFrom(x)
                            && !x.IsAbstract && !x.IsInterface)
                .Select(Activator.CreateInstance).Cast<IServiceInstaller>().ToList();
                
            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
