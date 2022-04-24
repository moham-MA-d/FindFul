using Core.Models.Entities.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations.Comments
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {   
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
        }
    }
}
