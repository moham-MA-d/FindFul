namespace DTO.Messages
{
    public class CreateMessageDTO
    {
        public string Body { get; set; }
        public string RecieverUsername { get; set; }
        public int RecieverId { get; set; }
    }
}
