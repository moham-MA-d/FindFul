
using Core.Models.Entities.User;
using System.Collections;
using System.Collections.Generic;

namespace Core.Models.Entities.Groups
{
    public class GroupAdmin : BaseFields
    {
        public int AdminId { get; set; }
        public int GroupId { get; set; }


        public AppUser TheAdmin { get; set; }
        public Group TheGroup { get; set; }

        public ICollection<GroupAdminGrant> TheGroupAdminGrantsList { get; set; }

    }
}
