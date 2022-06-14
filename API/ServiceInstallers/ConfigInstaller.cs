using API.Helpers;
using API.Services.Classes;
using API.Services.Interfaces;
using Core;
using Core.IServices.User;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.Classes.User;
using Service.Helpers;

namespace API.ServiceInstallers
{
    public class ConfigInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<CloudinarySetting>(configuration.GetSection("CloudinarySettings"));

            // Useful for http request. It's is equal to http request lifetime.
            // The point which we used Interface is that it would be much easier to test the application. 
            services.AddScoped<ITokenService, TokenService>();

            services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("DefaultLogger"));

            services.AddScoped<LogUserActivity>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Connection string is defined in appsetting.json
            // AddDbContext life time is Scoped
            services.AddDbContext<DataContext>(x =>
                //x.UseSqlServer(configuration.GetConnectionString("FindFulConnection"))
                x.UseNpgsql(configuration.GetConnectionString("PostgresConnection"))
                );
            //services.AddDbContext<DataContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Data")));
        }
    }
}
