using Microsoft.EntityFrameworkCore;
using Core.Models.Entities.User;
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
using Core.Models.Entities.SignalR;
using Data.Interceptors;
using Core.Models.Entities.Groups;
using Data.Configurations.Groups;

namespace Data
{
    public sealed class DataContext : IdentityDbContext<AppUser, AppRole, int, 
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new LoggingInterceptor());
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<UserPhoto> UserPhotos { get; set; }
        
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLiked> PostsLiked { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Follow> Follows { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<SignalRGroup> SignalRGroups { get; set; }
        public DbSet<SignalRConnection> SignalRConnections { get; set; }


        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<GroupAdmin> GroupAdmins { get; set; }
        public DbSet<GroupGrant> GroupGrants { get; set; }
        public DbSet<GroupAdminGrant> GroupAdminGrants { get; set; }


        //This method that is called when Context is created for the first time.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AppUserConfig());
            modelBuilder.ApplyConfiguration(new AppRoleConfig());
            modelBuilder.ApplyConfiguration(new AppUserRoleConfig());

            modelBuilder.ApplyConfiguration(new IdentityUserClaimConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserLoginConfig());
            modelBuilder.ApplyConfiguration(new IdentityRoleClaimConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserTokenConfig());

            modelBuilder.ApplyConfiguration(new UserPhotoConfig());

            modelBuilder.ApplyConfiguration(new PostConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new FollowConfig());
            modelBuilder.ApplyConfiguration(new PostLikedConfig());
            modelBuilder.ApplyConfiguration(new MessageConfig());
            modelBuilder.ApplyConfiguration(new SignalRGroupConfig());
            //modelBuilder.ApplyConfiguration(new RefreshTokenConfig());


            modelBuilder.Entity<Group>().UseTphMappingStrategy();
            modelBuilder.ApplyConfiguration(new GroupConfig());
            modelBuilder.ApplyConfiguration(new GroupAdminGrantConfig());


        }
    }
}
