using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Entities.SignalR
{
    public class SignalRConnection
    {
        public SignalRConnection()
        {
        }

        public SignalRConnection(string connectionId, string userId)
        {
            ConnectionId = connectionId;
            UserId = userId;
        }

        [Key]
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;

    }
}
