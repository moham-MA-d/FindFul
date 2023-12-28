

using Core.Models.Entities.User;
using System.Collections.Generic;
using static DTO.Enumerations.GroupEnums;

namespace Core.Models.Entities.Groups;

public class Group : BaseFields
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Username { get; set; }
    public GroupType Type { get; set; }

    public int CreatorUserId { get; set; }
    public AppUser TheCreatorUser { get; set; }

    public ICollection<GroupMember> TheMembersList { get; set; }
    public ICollection<GroupAdmin> TheAdminsList { get; set; }

}
