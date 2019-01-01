using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        public static IEnumerable<TSource> WhereMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            var max = source.Max(selector);
            return source.Where(x => selector(x).Equals(max));
        }
        public static IEnumerable<TSource> WhereMax<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            var max = source.Max(selector);
            return source.Where(x => selector(x).Equals(max));
        }
        public static IEnumerable<TSource> WhereMax<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            var max = source.Max(selector);
            return source.Where(x => selector(x).Equals(max));
        }
        public static IEnumerable<TSource> WhereMax<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            var max = source.Max(selector);
            return source.Where(x => selector(x).Equals(max));
        }
        public static IEnumerable<TSource> WhereMax<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            var max = source.Max(selector);
            return source.Where(x => selector(x).Equals(max));
        }
        public static IEnumerable<TSource> WhereMax<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var max = source.Max(selector);
            return source.Where(x => selector(x).Equals(max));
        }

    }
}
