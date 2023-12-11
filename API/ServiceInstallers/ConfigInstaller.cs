using System;
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

            // AddDbContext life time is Scoped
            services.AddDbContext<DataContext>(options =>
                    //x.UseSqlServer(configuration.GetConnectionString("FindFulConnection"))
                    //x.UseNpgsql(configuration.GetConnectionString("PostgresConnection"))
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                    string connectionString;

                    // Depending on if in development or production, use either Heroku-provided
                    // connection string, or development connection string from env var.
                    if (env == "Development")
                    {
                        options.UseNpgsql(configuration.GetConnectionString("FindFulConnection"));
                        options.LogTo(Console.WriteLine, LogLevel.Information);
                        //options.UseSqlServer(configuration.GetConnectionString("FindFulConnection"));
                    }
                    else
                    {
                        // Use connection string provided at runtime by Heroku.
                        var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                        // Parse connection URL to connection string for Npgsql
                        if (connectionUrl != null)
                        {
                            connectionUrl = connectionUrl.Replace("postgres://", string.Empty);
                            var pgUserPass = connectionUrl.Split("@")[0];
                            var pgHostPortDb = connectionUrl.Split("@")[1];
                            var pgHostPort = pgHostPortDb.Split("/")[0];
                            var pgDb = pgHostPortDb.Split("/")[1];
                            var pgUser = pgUserPass.Split(":")[0];
                            var pgPass = pgUserPass.Split(":")[1];
                            var pgHost = pgHostPort.Split(":")[0];
                            var pgPort = pgHostPort.Split(":")[1];

                            connectionString =
                                $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True";
                            options.UseSqlServer(connectionString);

                        }
                        else
                        {
                            throw new Exception("There is a problem in connection!");
                        }
                    }
                    // Whether the connection string came from the local development configuration file
                    // or from the environment variable from Heroku, use it to set up your DbContext.
                }
            );
        }
    }
}