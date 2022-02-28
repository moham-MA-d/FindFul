using Microsoft.EntityFrameworkCore;
using Core.Models.Entities.User;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class DataContext : DbContext
    {
        //private readonly IConfiguration _configuration;

        //public DataContext() {}
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserPhoto> Photos { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlite("name=DefaultConnection");
        //        //optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
        //    }
        //}
    }
}
