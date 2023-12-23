namespace Core.Models.Entities.Tag
{
    public class Tag : BaseFields
    {
        public string Name { get; set; }
        public int FirstUserId { get; set; }

        public int Count { get; set; }
    }
}
