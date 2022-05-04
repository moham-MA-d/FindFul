using Core.Models.Entities.User;
using System;

namespace Core.Models.Entities.Messages
{
    public class Message : BaseId
    {
        public string Body { get; set; }
        public DateTime? DateRecieved { get; set; }
        public DateTime? DateReaded { get; set; }
        //public DateTime? DateDeleted { get; set; }
        
        public bool SnderDeleted { get; set; }
        public bool RecieverDeleted { get; set; }


        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public AppUser TheSender { get; set; }

        public int RecieverId { get; set; }
        public string RecieverUsername { get; set; }
        public AppUser TheReciever { get; set; }
    }
}
