using Core.Models.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.User
{
    public class AppRoleConfig : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder
                .HasMany(x => x.TheUserRolesList)
                .WithOne(x => x.TheRole)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();
        }
    }
}
