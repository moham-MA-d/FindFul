using System.Collections.Generic;
using static DTO.Enumerations.UserEnums;

namespace Core.Models.Entities.Groups;

public class GroupGrant
{
    public int Id { get; set; }
    public AdminGrant AdminGrant { get; set; }
    public ICollection<GroupAdminGrant> TheGroupAdminGrantsLost { get; set; }

}
