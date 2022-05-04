using System;

namespace DTO.Messages
{
    public class MessageDTO
    {
        public string Body { get; set; }
        public DateTime? DateRecieved { get; set; }
        public DateTime? DateReaded { get; set; }


        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string SenderPhotoUrl { get; set; }

        public int RecieverId { get; set; }
        public string RecieverUsername { get; set; }
        public string RecieverPhotoUrl{ get; set; }
    }
}
