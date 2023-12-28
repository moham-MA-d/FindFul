
namespace Core.Models.Entities.Groups;

public class GroupAdminGrant : BaseFields
{
    public int GroupAdminId { get; set; }
    public int GroupGrantId { get; set; }

    public GroupAdmin TheGroupAdmin { get; set; }
    public GroupGrant TheGroupGrant { get; set; }
}
