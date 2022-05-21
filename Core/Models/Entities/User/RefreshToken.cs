using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Entities.User
{
    public class RefreshToken
    {
        [Key] public string Token { get; set; } = Guid.NewGuid().ToString();
        public string JwtId { get; set; }

        // IsUsed: If is used we should not issue another jwt with the same refreshToken
        public bool IsUsed { get; set; }
        
        // IsInvalidated: When we want a user provide its credential again. for ex. after changing password.
        public bool IsInvalidated { get; set; }
        public DateTime ExpireDateTime { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser TheUser { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
