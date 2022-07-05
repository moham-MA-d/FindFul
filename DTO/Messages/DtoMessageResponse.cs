using System;

namespace DTO.Messages
{
    public class DtoMessageResponse
    {
        public string Body { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? DateRecieved { get; set; }
        public DateTime? DateReaded { get; set; }


        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string SenderPhotoUrl { get; set; }

        public int ReceiverId { get; set; }
        public string ReceiverUsername { get; set; }
        public string ReceiverPhotoUrl { get; set; }
    }
}
