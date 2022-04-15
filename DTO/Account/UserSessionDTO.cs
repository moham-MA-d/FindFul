namespace DTO.Account
{
    public class UserSessionDTO
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public  Enumarations.UserEnums.Gender Gender { get; set; }
        public  Enumarations.UserEnums.Sex Sex { get; set; }
    }
}
