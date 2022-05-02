using Core.Models.Entities.Comments;
using Core.Models.Entities.User;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Core.Models.Entities.Posts
{
    public class Post : BaseId
    {
        public string Body { get; set; }
        public DateTime DeleteDateTime { get; set; }
        public int LikesCount { get; set; } = 0;
        public int FavesCount { get; set; } = 0;
        public int DislikesCount { get; set; } = 0;
        public int WowsCount { get; set; } = 0;
        public int LaughsCount { get; set; } = 0;
        public int SadsCount { get; set; } = 0;
        public int AngersCount { get; set; } = 0;
        public int CommentsCount { get; set; } = 0;
        public int ViewsIpCount { get; set; } = 0;
        public int ViewsUsersCount { get; set; } = 0;
        public int ViewsTotalCount { get; set; } = 0;

        public int AppUserId { get; set; }
        public AppUser TheAppUser { get; set; }

        public ICollection<Comment> TheCommentsList { get; set; }
        public ICollection<PostLiked> ThePostLikedList { get; set; }
    }
}
