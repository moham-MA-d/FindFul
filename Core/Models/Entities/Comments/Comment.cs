﻿using Core.Models.Entities.Posts;
using Core.Models.Entities.User;

namespace Core.Models.Entities.Comments
{
    public class Comment : BaseFields
    {
        public string Text { get; set; }

        public int UserId { get; set; }
        public AppUser TheAppUser { get; set; }
        public int PostId { get; set; }
        public Post ThePost { get; set; }
    }
}
