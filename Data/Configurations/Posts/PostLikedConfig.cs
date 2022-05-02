using Core.Models.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations.Posts
{
	public class PostLikedConfig : IEntityTypeConfiguration<PostLiked>
	{
		public void Configure(EntityTypeBuilder<PostLiked> builder)
		{
			builder.HasKey(x => new { x.PostId, x.AppUserId });

			builder.HasOne(x => x.TheAppUser)
				.WithMany(x => x.ThePostLikedList)
				.HasForeignKey(x => x.AppUserId);

			builder.HasOne(x => x.ThePost)
				.WithMany(x => x.ThePostLikedList)
				.HasForeignKey(x => x.PostId);
		}
	}
}
