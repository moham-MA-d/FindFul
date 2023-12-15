using API.ServiceInstallers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;

namespace API.Initializers
{
    public static class ServiceInitializer
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Program).Assembly.ExportedTypes
                .Where(x => typeof(IServiceInstaller).IsAssignableFrom(x)
                            && !x.IsAbstract && !x.IsInterface)
                .Select(Activator.CreateInstance).Cast<IServiceInstaller>().ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
