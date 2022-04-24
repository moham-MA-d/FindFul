using DTO._Enumarations;

namespace DTO.Pagination
{
    public class PostParameters : PageParameters
    {
        public PostEnums.OrderBy OrderBy { get; set; }
    }
}
