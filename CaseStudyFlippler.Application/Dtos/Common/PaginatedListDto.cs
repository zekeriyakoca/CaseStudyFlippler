using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyFlippler.Application.Dtos
{
    public class PaginatedList<T>
    {
        public PaginatedList()
        {

        }
        public PaginatedList(IList<T> items, int page, int pageSize, int totalItemCount)
        {
            Items = items;
            Page = page < 1 ? 1 : page;
            TotalItemCount = totalItemCount;
            PageSize = pageSize == 0 ? 1 : pageSize;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public IList<T> Items { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPage
        {
            get
            {
                if (Items?.Count > 0)
                {
                    var temp = Math.Ceiling(new decimal(TotalItemCount) / new decimal(PageSize));
                    return (int)temp;
                }
                return 0;
            }
        }
        public bool HasNext
        {
            get
            {
                return TotalPage > Page;
            }
        }
        public bool HasPrev
        {
            get
            {
                return Page > 1;
            }
        }

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count(); // Expected to fire lightweight query. If not, it may be better to use next Create method.
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, pageIndex, pageSize, count);
        }
        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, pageIndex, pageSize, totalCount);
        }
        public static PaginatedList<T> Clone(IList<T> source, int page, int pageSize)
        {
            return new PaginatedList<T>(source, page, pageSize, source.Count);
        }


    }
}
