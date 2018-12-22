using Dawnx.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        /// <summary>
        /// Projects page elements of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="pageNumber">'pageNumber' starts at 1</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedEnumerable<TSource> SelectPage<TSource>(this IEnumerable<TSource> @this, int pageNumber, int pageSize)
            => new PagedEnumerable<TSource>(@this, pageNumber, pageSize);

        /// <summary>
        /// Calculates the max page number through the specified page size.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static int PageCount<TSource>(this IEnumerable<TSource> @this, int pageSize)
        {
            int count = 0;
            switch (@this)
            {
                case TSource[] array: count = array.Length; break;
                case ICollection<TSource> collection: count = collection.Count; break;
                case IQueryable<TSource> querable: count = querable.Count(); break;
                default: count = @this.Count(); break;
            }
            return (int)Math.Ceiling((double)count / pageSize);
        }

        public static IEnumerable<TSource> WhereNot<TSource>(this IEnumerable<TSource> @this, Expression<Func<TSource, bool>> predicate)
            => @this.Where(Expression.Lambda<Func<TSource, bool>>(Expression.Not(predicate.Body), predicate.Parameters).Compile());

    }

}
