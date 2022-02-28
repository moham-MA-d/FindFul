using DTO.Media;
using System;
using System.Collections.Generic;

namespace DTO.Account
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public int Sex { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        public string Info { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int Age { get; set; }
        public DateTime LastActivity { get; set; }
        public ICollection<PhotoDTO> Photos { get; set; }
    }
}
