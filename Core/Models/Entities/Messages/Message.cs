using Core.Models.Entities.User;
using System;

namespace Core.Models.Entities.Messages
{
    public class Message : BaseFields
    {
        public string Body { get; set; }

        public string SenderUsername { get; set; }

        public int SenderId { get; set; }
        public AppUser TheSender { get; set; }
        public bool SenderDeleted { get; set; }


        public string ReceiverUsername { get; set; }
        public int ReceiverId { get; set; }
        public AppUser TheReceiver { get; set; }
        public bool ReceiverDeleted { get; set; }


        public DateTime? DateReceived { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime? DateDeleted { get; set; }


    }
}
