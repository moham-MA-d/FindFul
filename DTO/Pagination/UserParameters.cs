using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Pagination
{
    public class UserParameters : PageParameters  
    {
        public string CurrentUsername { get; set; }
        public Enumarations.UserEnums.Gender Gender { get; set; }
        public Enumarations.UserEnums.Sex Sex { get; set; }
        public Enumarations.UserEnums.OrderBy OrderBy { get; set; }

        public int MinAge { get; set; } = 10;
        public int MaxAge { get; set; } = 120;

    }
}
