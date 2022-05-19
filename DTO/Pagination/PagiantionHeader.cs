namespace DTO.Pagination
{
    public class PagiantionHeader
    {
        public PagiantionHeader(int pageIndex, int itemsPerPage, int totalItems, int totalPages)
        {
            PageIndex = pageIndex;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        public int PageIndex { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}
