using System;

namespace API.Entities
{
    public class BaseId
    {
        public int Id { get; set; }
        public Guid GuId { get; set; } = Guid.NewGuid();
    }
}