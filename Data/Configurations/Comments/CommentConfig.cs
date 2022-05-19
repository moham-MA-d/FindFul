using Core.Models.Entities.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Comments
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {   
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
        }
    }
}
