using System;

namespace Core.Models.Entities
{
    public class BaseId
    {
        public int Id { get; set; }
        public Guid GuId { get; set; } = Guid.NewGuid();
        public bool IsDelete { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTime CreateDateTime { get; set; } = DateTime.Now;

    }
}