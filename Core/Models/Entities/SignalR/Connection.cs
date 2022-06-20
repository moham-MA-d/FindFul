using System.ComponentModel.DataAnnotations;

namespace Core.Models.Entities.SignalR
{
    public class SignalRConnection
    {
        public SignalRConnection()
        {
        }

        public SignalRConnection(string connectionId, string username)
        {
            ConnectionId = connectionId;
            Username = username;
        }

        [Key]
        public string ConnectionId { get; set; }
        public string Username { get; set; }
    }
}
