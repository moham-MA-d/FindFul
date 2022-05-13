using Microsoft.AspNetCore.Identity;

namespace Core.Models.Entities.User
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser TheUser  { get; set; }
        public AppRole TheRole { get; set; }
    }
}
