using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Models.Entities.User;

namespace Data.Seed
{
    public static class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var userSeedData = await System.IO.File.ReadAllTextAsync(@"D:\dotnet Core\Findful\Data\Seed\UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userSeedData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}