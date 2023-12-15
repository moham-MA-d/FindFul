using System;
using System.IO;
using System.Reflection;
using API.Helpers;
using API.Helpers.Authentication;
using API.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Service.Helpers;
using Swashbuckle.AspNetCore.Filters;

namespace API.ServiceInstallers
{
    public class BasicInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddCors();

            services.AddLogging();

            //IdentityServiceExtension.SetLogger(services.BuildServiceProvider().GetRequiredService<ILogger>());
            IdentityService.SetLogger(services.BuildServiceProvider().GetRequiredService<ILoggerFactory>().CreateLogger("IdentityServiceExtension"));

            services.AddIdentityServices(configuration);

            services.AddSingleton(_ => configuration);

            services.AddMvc(opt => { opt.EnableEndpointRouting = false; });
            
            services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>()
                .CreateLogger("DefaultLogger"));
            services.AddScoped<LogUserActivity>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddSingleton<OnlineTracker>();
            services.AddSignalR(e => {
                e.EnableDetailedErrors = true;
                //e.MaximumReceiveMessageSize = 102400000;
                //e.EnableDetailedErrors = true;
            });        
        }
    }
}
