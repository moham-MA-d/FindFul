using Microsoft.EntityFrameworkCore;
using Core.Models.Entities.Posts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Posts
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
               .HasMany(x => x.TheCommentsList)
               .WithOne(x => x.ThePost)
               .HasForeignKey(x => x.PostId)
               .OnDelete(DeleteBehavior.NoAction);


            builder.Property(x => x.Body)
                .IsRequired();


        }
    }
}
