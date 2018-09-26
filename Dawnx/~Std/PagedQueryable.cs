using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public class PagedQueryable<T> : IPageable<T>, IQueryable<T>
    {
        public IQueryable<T> Items;
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int PageCount { get; private set; }
        public bool IsFristPage => PageNumber == 1;
        public bool IsLastPage => PageNumber == PageCount;

        public Type ElementType => Items.ElementType;
        public Expression Expression => Items.Expression;
        public IQueryProvider Provider => Items.Provider;

        public PagedQueryable(IQueryable<T> source, int page, int pageSize)
        {
            PageNumber = page;
            PageSize = pageSize;
            PageCount = source.PageCount(pageSize);
            Items = source.Skip((PageNumber - 1) * PageSize).Take(PageSize);
        }

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }

}
