using DTO._Enumarations;

namespace DTO.Account
{
    public class UserSessionDTO
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public UserEnums.Gender Gender { get; set; }
        public UserEnums.Sex Sex { get; set; }
    }
}
