using Core.Models.Entities.Comments;
using Core.Models.Entities.Follows;
using Core.Models.Entities.Groups;
using Core.Models.Entities.Messages;
using Core.Models.Entities.Posts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Core.Models.Entities.User
{
    public class AppUser : IdentityUser<int>
    {
        public Guid GuId { get; set; } = Guid.NewGuid();
        public bool IsDelete { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;

        public string Phone { get; set; }
        public string ReferalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int Sex { get; set; }
        public int Gender { get; set; }
        public string Info { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;
        public string ProfilePhotoUrl { get; set; }
        public string ProfilePhotoUrlPublicId { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string CoverPhotoUrlPublicId { get; set; }


        public ICollection<UserPhoto> TheUserPhotosList { get; set; }
        public ICollection<Post> ThePostsList { get; set; }
        public ICollection<Group> TheGroupsList { get; set; }
        public ICollection<GroupAdmin> TheGroupAdminsList { get; set; }
        public ICollection<RefreshToken> TheRefreshTokensList { get; set; }
        public ICollection<Comment> TheCommentsList { get; set; }
		public ICollection<Follow> TheFollowerList { get; set; }
		public ICollection<Follow> TheFollowingList { get; set; }
		public ICollection<PostLiked> ThePostLikedList { get; set; }
		public ICollection<Message> TheSentMessagesList { get; set; }
		public ICollection<Message> TheReceivedMessagesList { get; set; }
        public virtual ICollection<AppUserRole> TheUserRolesList { get; set; }

    }
}
