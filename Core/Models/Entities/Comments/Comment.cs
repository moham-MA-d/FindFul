﻿using Core.Models.Entities.Posts;
using Core.Models.Entities.User;
using System;

namespace Core.Models.Entities.Comments
{
    public class Comment : BaseId
    {
        public string Text { get; set; }

        public int AppUserId { get; set; }
        public AppUser TheAppUser { get; set; }

        public int PostId { get; set; }
        public Post ThePost { get; set; }
    }
}
