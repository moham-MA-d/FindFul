namespace DTO.Messages
{
    public class DtoCreateMessage
    {
        public string Body { get; set; }
        public string ReceiverUsername { get; set; }
        public int ReceiverId { get; set; }
    }
}
