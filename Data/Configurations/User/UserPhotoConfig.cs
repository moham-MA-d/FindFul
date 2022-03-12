using Core.Models.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.User
{
    public class UserPhotoConfig : IEntityTypeConfiguration<UserPhoto>
    {
        public void Configure(EntityTypeBuilder<UserPhoto> builder)
        {
            builder.Property(x => x.Url)
                .HasColumnType("varchar")
                .HasMaxLength(256);


            builder.Property(x => x.PublicId)
                .HasColumnType("varchar")
                .HasMaxLength(256);

        }
    }
}
