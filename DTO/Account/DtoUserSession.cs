using DTO.Enumerations;

namespace DTO.Account
{
    public class DtoUserSession
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public UserEnums.Gender Gender { get; set; }
        public UserEnums.Sex Sex { get; set; }
    }
}
