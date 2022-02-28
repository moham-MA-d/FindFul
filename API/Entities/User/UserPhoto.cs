namespace API.Entities.User
{
    public class UserPhoto : BaseId
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string publicId { get; set; }
        public bool IsDelete { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}