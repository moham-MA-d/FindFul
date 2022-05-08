using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Models.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace Data.Seed
{
    public static class Seed
    {
        // We get UserManager From Identity so we can pass it here
        public static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userSeedData = await System.IO.File.ReadAllTextAsync(@"D:\dotnet Core\Findful\Data\Seed\UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userSeedData);

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}