using DTO._Enumarations;

namespace DTO.Account
{
    public class MemberUpdateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public UserEnums.Sex Sex { get; set; }
        public UserEnums.Gender Gender { get; set; }
        public string Info { get; set; }

    }
}
