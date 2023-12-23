namespace Core.Models.Entities.User
{
    public class UserPhoto : BaseFields
    {

        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        public int UserId { get; set; }
        public AppUser TheUser { get; set; }
    }
}