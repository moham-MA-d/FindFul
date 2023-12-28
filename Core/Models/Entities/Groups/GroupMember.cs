using Core.Models.Entities.User;

namespace Core.Models.Entities.Groups;

public class GroupMember : BaseFields
{
    public int MemberId { get; set; }
    public int GroupId { get; set; }

    public AppUser TheMember { get; set; }
    public Group TheGroup { get; set; }
}
