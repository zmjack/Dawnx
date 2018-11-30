using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public class PagedQueryable<T> : PagedEnumerable<T>, IQueryable<T>
    {
        public Type ElementType => (Items as IQueryable<T>).ElementType;
        public Expression Expression => (Items as IQueryable<T>).Expression;
        public IQueryProvider Provider => (Items as IQueryable<T>).Provider;

        public PagedQueryable(IQueryable<T> source, int page, int pageSize)
        {
            PageNumber = page;
            PageSize = pageSize;
            PageCount = source.PageCount(pageSize);
            Items = source.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToArray();
        }

    }

}
