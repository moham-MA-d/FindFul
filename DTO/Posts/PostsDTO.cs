using DTO.Account;
using System.ComponentModel.DataAnnotations;

namespace DTO.Posts
{
    public class PostsDTO
    {
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }

        public MemberDTO TheAppUser { get; set; }
    }
}
