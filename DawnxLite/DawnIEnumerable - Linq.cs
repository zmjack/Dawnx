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

        /// <summary>
        /// Returns distinct elements from a sequence by using a specified properties to compare values.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctByValue<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, object>> expression)
                => Enumerable.Distinct(source, new ExactEqualityComparer<TSource>(expression));

        /// <summary>
        /// Produces the set difference of two sequences by using the specified properties to compare values.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ExceptByValue<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Expression<Func<TSource, object>> expression)
            => Enumerable.Except(first, second, new ExactEqualityComparer<TSource>(expression));

        /// <summary>
        /// Produces the set union of two sequences by using a specified properties.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> UnionByValue<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Expression<Func<TSource, object>> expression)
            => Enumerable.Union(first, second, new ExactEqualityComparer<TSource>(expression));

        /// <summary>
        /// Produces the set intersection of two sequences by using the specified properties to compare values.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> IntersectByValue<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Expression<Func<TSource, object>> expression)
            => Enumerable.Intersect(first, second, new ExactEqualityComparer<TSource>(expression));
    }

}
