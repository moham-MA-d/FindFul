using System;

namespace Core.Models.Entities
{
    public class BaseId
    {
        public int Id { get; set; }
        public Guid GuId { get; set; } = Guid.NewGuid();
    }
}