using Core.Models.Entities.User;
using System;

namespace Core.Models.Entities.Follows
{
	public class Follow
	{
		public int FollowerId { get; set; }
		public int FollowingId { get; set; }
        public bool IsActive { get; set; }
		public DateTime CereateDateTime { get; set; } = DateTime.Now;

        public AppUser TheFollower { get; set; }
		public AppUser TheFollowing { get; set; }
	}
}
