using Microsoft.EntityFrameworkCore;
using Core.Models.Entities.User;
using Microsoft.Extensions.Configuration;
using Data.Configurations.User;

namespace Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            //ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfig());
            modelBuilder.ApplyConfiguration(new UserPhotoConfig());
        }
    }
}
