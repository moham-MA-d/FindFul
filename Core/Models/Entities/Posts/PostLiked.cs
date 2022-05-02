using Core.Models.Entities.User;
using System;

namespace Core.Models.Entities.Posts
{
	public class PostLiked
	{
		public int PostId { get; set; }
		public int AppUserId { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime CreateDateTime { get; set; } = DateTime.Now;

		public Post ThePost { get; set; }
		public AppUser TheAppUser { get; set; }
	}
}
