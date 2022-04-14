using System;
using System.Collections.Generic;

namespace DTO.Pagination
{
    public class PagedListBase<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public PagedListBase(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            PageSize = pageSize;
            TotalItems = count;
            AddRange(items);
        }
    }
}
