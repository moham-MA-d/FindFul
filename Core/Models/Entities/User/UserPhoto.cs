namespace Core.Models.Entities.User
{
    public class UserPhoto : BaseId
    {

        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public bool IsDelete { get; set; }

        public AppUser TheAppUser { get; set; }
        public int AppUserId { get; set; }
    }
}