namespace DTO.Pagination
{
    public class PageParameters
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        private int _pageSize = 10;
    }
}
