using Core.Models.Entities.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Messages
{
    public class SignalRGroupConfig : IEntityTypeConfiguration<SignalRGroup>
    {
        public void Configure(EntityTypeBuilder<SignalRGroup> builder)
        {
            builder
                .HasMany(x => x.Connections)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
