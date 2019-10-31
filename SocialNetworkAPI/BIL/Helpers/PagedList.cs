using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BIL.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList(List<T> items, int count, int currentPage, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public static PagedList<T> Create(IEnumerable<T> source,
            int currentPage, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, currentPage, pageSize);
        }
    }
}