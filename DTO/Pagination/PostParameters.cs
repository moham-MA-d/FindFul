using DTO.Enumerations;

namespace DTO.Pagination
{
    public class PostParameters : PageParameters
    {
        public PostEnums.OrderBy OrderBy { get; set; }
    }
}
