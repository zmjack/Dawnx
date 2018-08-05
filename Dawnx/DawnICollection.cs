using System.Collections.Generic;

namespace Dawnx
{
    public static class DawnICollection
    {
        /// <summary>
        /// Projects page elements of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="pageNumber">'pageNumber' starts at 1</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedEnumerable<TSource> SelectPage<TSource>(this ICollection<TSource> @this, int pageNumber, int pageSize)
            => new PagedEnumerable<TSource>(@this, pageNumber, pageSize, @this.Count);

    }
}
