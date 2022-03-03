using DTO.Media;
using System;
using System.Collections.Generic;

namespace DTO.Account
{
    public class MemberDTO : MemberUpdateDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public int Sex { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int Age { get; set; }
        public DateTime LastActivity { get; set; }
        public ICollection<PhotoDTO> Photos { get; set; }
    }
}
