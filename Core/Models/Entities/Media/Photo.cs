namespace Core.Models.Entities.Media
{
    public class Photo : BaseId
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}
