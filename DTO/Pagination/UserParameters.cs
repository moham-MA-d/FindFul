using DTO.Enumerations;

namespace DTO.Pagination
{
    public class UserParameters : PageParameters  
    {
        public string CurrentUsername { get; set; }
        public string Username { get; set; }
        public UserEnums.Gender Gender { get; set; }
        public UserEnums.Sex Sex { get; set; }
        public UserEnums.OrderBy OrderBy { get; set; }

        public int MinAge { get; set; } = 10;
        public int MaxAge { get; set; } = 120;

    }
}
