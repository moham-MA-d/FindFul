using Core.Models.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Posts
{
	public class PostLikedConfig : IEntityTypeConfiguration<PostLiked>
	{
		public void Configure(EntityTypeBuilder<PostLiked> builder)
		{
			builder.HasKey(x => new { x.PostId, x.UserId });

			builder.HasOne(x => x.TheAppUser)
				.WithMany(x => x.ThePostLikedList)
				.HasForeignKey(x => x.UserId);

			builder.HasOne(x => x.ThePost)
				.WithMany(x => x.ThePostLikedList)
				.HasForeignKey(x => x.PostId);
		}
	}
}
