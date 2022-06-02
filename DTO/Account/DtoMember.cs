using DTO.Account.Photo;
using System;
using System.Collections.Generic;

namespace DTO.Account
{
    public class DtoMember : DtoMemberUpdate
    {
        
        public string Email { get; set; }
        public string CoverPhotoUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int Age { get; set; }
        public bool IsFollowed { get; set; }
        public DateTime LastActivity { get; set; }
        public ICollection<DtoMemberPhoto> Photos { get; set; }
    }
}
