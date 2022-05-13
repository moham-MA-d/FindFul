using Core.Models.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.User
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Users");

            builder
                .HasMany(x => x.TheUserRolesList)
                .WithOne(x => x.TheUser)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder
                .HasMany(x => x.ThePostsList)
                .WithOne(x => x.TheUser)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.TheUserPhotosList)
                .WithOne(x => x.TheUser)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            #region Properties

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(70);

            builder.Property(x => x.City)
                .HasMaxLength(50);

            builder.Property(x => x.Country)
                .HasMaxLength(50);

            builder.Property(x => x.FirstName)
                .HasMaxLength(60);

            builder.Property(x => x.MiddleName)
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .HasMaxLength(60);


            builder.Property(x => x.ReferalCode)
                .HasColumnType("varchar")
                .HasMaxLength(10);

            builder.Property(x => x.Info)
                .HasMaxLength(2000);

            builder.Property(x => x.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(15);
            #endregion

        }
    }
}
