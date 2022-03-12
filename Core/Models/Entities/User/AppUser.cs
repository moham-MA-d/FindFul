using System;
using System.Collections.Generic;

namespace Core.Models.Entities.User
{
    public class AppUser : BaseId
    {
        public string UserName { get; set; } = new string("user" + new Random(14));
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ReferalCode { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int Sex { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        public string Info { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public DateTime DateOfBirth { get; set; }
        public DateTime LastActivity { get; set; } = DateTime.Now;

        public ICollection<UserPhoto> TheUserPhotosList { get; set; }

    }
}
