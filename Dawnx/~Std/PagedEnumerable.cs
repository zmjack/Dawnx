using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx
{
    public class PagedEnumerable<T> : IPageable<T>, IEnumerable<T>
    {
        public IEnumerable<T> Items { get; protected set; }
        public int PageNumber { get; protected set; }
        public int PageSize { get; protected set; }
        public int PageCount { get; protected set; }
        public bool IsFristPage => PageNumber == 1;
        public bool IsLastPage => PageNumber == PageCount;

        protected PagedEnumerable() { }

        public PagedEnumerable(IEnumerable<T> source, int page, int pageSize)
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
