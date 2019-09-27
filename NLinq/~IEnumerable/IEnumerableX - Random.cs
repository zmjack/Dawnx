using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NLinq
{
    public static partial class IEnumerableX
    {
        /// <summary>
        /// Select the specified number of random record from a source set.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Random<TSource>(this IEnumerable<TSource> @this, int count)
        {
            return @this.OrderBy(x => Guid.NewGuid()).Take(count);
        }

    }

}
