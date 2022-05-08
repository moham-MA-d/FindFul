using Core.Models.Entities.User;
using System;

namespace Core.Models.Entities.Posts
{
	public class PostLiked
	{
        public bool IsActive { get; set; } = false;
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
		public int PostId { get; set; }
		public Post ThePost { get; set; }
		public int UserId { get; set; }
		public AppUser TheAppUser { get; set; }
	}
}
