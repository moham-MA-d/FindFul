using Core.Models.Entities.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations.Messages
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasOne(m => m.TheSender)
                .WithMany(u => u.TheSentMessagesList)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(m => m.TheReciever)
               .WithMany(u => u.TheRecievedMessagesList)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
