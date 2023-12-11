using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Models.Entities.User;
using Microsoft.AspNetCore.Identity;
using System;

namespace Data.Seed
{
    public static class Seed
    {
        // We get UserManager From Identity so we can pass it here
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userSeedData = await System.IO.File.ReadAllTextAsync("../Data/Seed/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<AppUser>>(userSeedData);

            var roles = new List<AppRole>()
            {
                new AppRole {Name = "Admin", NormalizedName = "ADMIN"},
                new AppRole {Name = "Moderator", NormalizedName = "MODERATOR"},
                new AppRole {Name = "DtoMember", NormalizedName = "MEMBER"}
            };

            foreach (var item in roles)
                await roleManager.CreateAsync(item);

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                user.CreateDateTime = user.CreateDateTime.ToUniversalTime();
                user.LastActivity = user.LastActivity.ToUniversalTime();
                user.DateOfBirth = user.DateOfBirth.ToUniversalTime();

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "DtoMember");
            }

            var admin = new AppUser
            {
                UserName = "Admin",
                ProfilePhotoUrl = "/assets/images/user.png",
            };
            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
        }
    }
}
