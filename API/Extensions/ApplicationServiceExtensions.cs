using API.Helpers;
using API.Services.Classes;
using API.Services.Interfaces;
using Core;
using Core.IRepositories.User;
using Core.IService.User;
using Core.IServices.Mapper;
using Core.IServices.User;
using Data;
using Data.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.Classes.Mapper;
using Service.Classes.User;
using Service.Helpers;

namespace API.Findful.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySetting>(configuration.GetSection("CloudinarySettings"));

            // Useful for http request. It's is equal to http request lifetime.
            // The point which we used Interface is that it would be much easier to test the application. 
            services.AddScoped<ITokenService, TokenService>();

            services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("DefaultLogger"));
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserPhotoRepository, UserPhotoRepository>();
            services.AddScoped<IUserPhotoService, UserPhotoService>();

            services.AddScoped<IMapperService, MapperService>();
            services.AddScoped<IPhotoServiceAPI, PhotoServiceAPI>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>(); 

            //connection string is defined in appsetting.json
            services.AddDbContext<DataContext>(x => x.UseSqlServer(configuration.GetConnectionString("FindFulConnection")));
            //services.AddDbContext<DataContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Data")));

            return services;
        }
    }
}
