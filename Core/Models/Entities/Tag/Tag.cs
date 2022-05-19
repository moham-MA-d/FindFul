namespace Core.Models.Entities.Tag
{
    public class Tag : BaseId
    {
        public string Name { get; set; }
        public int FirstUserId { get; set; }

        public int Count { get; set; }
    }
}
