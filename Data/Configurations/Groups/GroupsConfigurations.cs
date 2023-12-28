using Core.Models.Entities.Groups;
using DTO.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DTO.Enumerations.GroupEnums;

namespace Data.Configurations.Groups;

public class GroupConfig : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasDiscriminator<GroupType>(x => x.Type)
            .HasValue<PrivateGroup>(GroupType.Private)
            .HasValue<PublicGroup>(GroupType.Public)
            .HasValue<Group>(GroupType.None);


        builder
            .HasMany(g => g.TheMembersList)
            .WithOne(gm => gm.TheGroup)
            .HasForeignKey(gm => gm.GroupId);

        builder
            .HasMany(g => g.TheAdminsList)
            .WithOne(ga => ga.TheGroup)
            .HasForeignKey(ga => ga.GroupId);


        builder.HasOne(g => g.TheCreatorUser)
            .WithMany(gc => gc.TheGroupsList)
            .HasForeignKey(gc => gc.CreatorUserId);
    }
}

public class GroupAdminGrantConfig : IEntityTypeConfiguration<GroupAdminGrant>
{
    public void Configure(EntityTypeBuilder<GroupAdminGrant> builder)
    {
        builder
           .HasOne(gag => gag.TheGroupAdmin)
           .WithMany(ga => ga.TheGroupAdminGrantsList)
           .HasForeignKey(ga => ga.GroupAdminId);

        builder
          .HasOne(gag => gag.TheGroupGrant)
          .WithMany(gg => gg.TheGroupAdminGrantsList)
          .HasForeignKey(ga => ga.GroupGrantId);
    }
}

