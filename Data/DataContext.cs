using Microsoft.EntityFrameworkCore;
using Core.Models.Entities.User;
using Microsoft.Extensions.Configuration;
using Data.Configurations.User;
using Core.Models.Entities.Posts;
using Core.Models.Entities.Comments;
using Data.Configurations.Posts;
using Data.Configurations.Comments;

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
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfig());
            modelBuilder.ApplyConfiguration(new UserPhotoConfig());
            modelBuilder.ApplyConfiguration(new PostConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
        }
    }
}
