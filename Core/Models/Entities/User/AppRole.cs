using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Core.Models.Entities.User
{
    public class AppRole : IdentityRole<int>
    {
        public virtual ICollection<AppUserRole> TheUserRolesList { get; set; }
    }
}
