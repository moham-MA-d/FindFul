using Data;
using Data.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API
{
    public class Program
    {
        // this method is outside of middleware, so we don't access to global error exception and we need try, catch
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            // create a scope for services that we need.
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                 var context = services.GetRequiredService<DataContext>();
                 // apply pending migrations to the database. and create database if it dose not alraedy exist
                 await context.Database.MigrateAsync();
                 await Seed.SeedUsers(context);
            }
            catch (System.Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during migration");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}