using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Pagination
{
    public class ScrollParameters
    {
        public int Skip { get; set; }
        public int Take { get; set; } = 20;
    }
}
