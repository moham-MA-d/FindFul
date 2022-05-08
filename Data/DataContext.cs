using Microsoft.EntityFrameworkCore;
using Core.Models.Entities.User;
using Microsoft.Extensions.Configuration;
using Data.Configurations.User;
using Core.Models.Entities.Posts;
using Core.Models.Entities.Comments;
using Data.Configurations.Posts;
using Data.Configurations.Comments;
using Core.Models.Entities.Follows;
using Data.Configurations.Follows;
using Core.Models.Entities.Messages;
using Data.Configurations.Messages;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Design;

namespace Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, 
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }


        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<PostLiked> PostsLiked { get; set; }
        public DbSet<Message> Messages { get; set; }


        //This is the method that is called when an entity is created.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfig());
            modelBuilder.ApplyConfiguration(new AppRoleConfig());
            modelBuilder.ApplyConfiguration(new UserPhotoConfig());
            modelBuilder.ApplyConfiguration(new PostConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new FollowConfig());
            modelBuilder.ApplyConfiguration(new PostLikedConfig());
            modelBuilder.ApplyConfiguration(new MessageConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
