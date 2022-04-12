using DTO.Account.Photo;
using DTO.Enumarations;
using System;
using System.Collections.Generic;

namespace DTO.Account
{
    public class MemberDTO : MemberUpdateDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string CoverPhotoUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int Age { get; set; }
        public DateTime LastActivity { get; set; }
        public ICollection<MemberPhotoDTO> Photos { get; set; }
    }
}
