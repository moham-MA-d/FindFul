using Core.Models.Entities.Follows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Follows
{
	public class FollowConfig : IEntityTypeConfiguration<Follow>
	{
		public void Configure(EntityTypeBuilder<Follow> builder)
		{
			builder.HasKey(x => new { x.FollowerId, x.FollowingId });

			builder.HasOne(x => x.TheFollowing)
				.WithMany(x => x.TheFollowerList)
				.HasForeignKey(x => x.FollowerId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(x => x.TheFollower)
				.WithMany(x => x.TheFollowingList)
				.HasForeignKey(x => x.FollowingId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
