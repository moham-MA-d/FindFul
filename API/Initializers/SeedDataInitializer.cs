using Core.Models.Entities.User;
using Data.Seed;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Initializers;

public static class SeedDataInitializer
{
    public static async Task<WebApplication> ConfigureSeedData(this WebApplication app)
    {
        // Database migration and seeding
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<DataContext>();
            var userManagerService = services.GetRequiredService<UserManager<AppUser>>();
            var roleManagerService = services.GetRequiredService<RoleManager<AppRole>>();
            await context.Database.MigrateAsync();
            await Seed.SeedUsers(userManagerService, roleManagerService);
        }
        catch (System.Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
        }

        return app;
    }
}