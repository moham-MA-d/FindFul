using Core.Models.Entities.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Messages
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasOne(m => m.TheSender)
                .WithMany(u => u.TheSentMessagesList)
                .HasForeignKey(u => u.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(m => m.TheReciever)
               .WithMany(u => u.TheReceivedMessagesList)
               .HasForeignKey(u => u.RecieverId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
