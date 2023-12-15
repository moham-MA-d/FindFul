using DTO.Account;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Posts
{
    public class DtoPostResponse
    {
        public int Id { get; set; }  

        [Required]
        public string Body { get; set; }
        public bool IsLiked { get; set; } = false;
        public int LikesCount { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
        public Account.DtoMember TheUser { get; set; }
    }
}
