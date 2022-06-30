using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Entities.SignalR
{
    public class SignalRGroup
    {
        public SignalRGroup()
        {
        }

        public SignalRGroup(string name)
        {
            Name = "grp-" + name;
        }

        [Key]
        public string Name { get; set; }
        public ICollection<SignalRConnection> Connections { get; set; } = new List<SignalRConnection>();

        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
    }
}
