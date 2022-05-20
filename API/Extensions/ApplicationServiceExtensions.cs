using System;
using System.Linq;
using API.Helpers;
using API.ServiceInstallers;
using API.Services.Classes;
using API.Services.Interfaces;
using Core;
using Core.IRepositories.Follows;
using Core.IRepositories.Messages;
using Core.IRepositories.Posts;
using Core.IRepositories.User;
using Core.IServices.Follows;
using Core.IServices.Mapper;
using Core.IServices.Messages;
using Core.IServices.Posts;
using Core.IServices.User;
using Data;
using Data.Repositories.Follows;
using Data.Repositories.Messages;
using Data.Repositories.Posts;
using Data.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.Classes.Follows;
using Service.Classes.Mapper;
using Service.Classes.Messages;
using Service.Classes.Posts;
using Service.Classes.User;
using Service.Helpers;

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
