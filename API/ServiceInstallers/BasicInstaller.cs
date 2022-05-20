using API.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.ServiceInstallers
{
    public class BasicInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo { Title = "Findful API", Version = "V1" });
                options.CustomSchemaIds(type => type.ToString());
            });
            services.AddCors();

            services.AddIdentityServices(configuration);

            services.AddSingleton(_ => configuration);
        }
    }
}
