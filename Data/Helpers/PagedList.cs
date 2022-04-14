using DTO.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Helpers
{
    public class PagedList<T> : PagedListBase<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageIndex, int pageSize) : base(items, count, pageIndex, pageSize) { }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
