using API.Services.Classes;
using API.Services.Interfaces;
using DTO.SocialNetworks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.ServiceInstallers
{
    public class FacebookInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var facebookAuthSettings = new FacebookAuthSetting();
            // Bind() will find a section called `FacebookAuthSetting` in app.settings abd bind json object to this C# Class
            configuration.Bind(nameof(FacebookAuthSetting), facebookAuthSettings);
            services.AddSingleton(facebookAuthSettings);

            services.AddHttpClient();
            services.AddSingleton<IFacebookService, FacebookService>();

        }
    }
}
