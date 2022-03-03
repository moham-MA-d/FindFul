using API.Services.Classes;
using API.Services.Interfaces;
using Core;
using Core.IRepositories.User;
using Core.IService.User;
using Core.Iservices.Mapper;
using Data;
using Data.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.Classes.Mapper;
using Service.Classes.User;
using Service.Helpers;

namespace API.Findful.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Useful for http request. It's is equal to http request lifetime.
            // The point which we used Interface is that it would be much easier to test the application. 
            services.AddScoped<ITokenService, TokenService>();

            services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("DefaultLogger"));
            services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMapperService, MapperService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            //connection string is defined in appsetting.json
            //services.AddDbContext<DataContext>(x => x.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DataContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Data")));

            return services;
        }
    }
}
