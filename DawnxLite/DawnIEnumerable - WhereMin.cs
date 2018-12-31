using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        public static IEnumerable<TSource> WhereMin<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            var min = source.Min(selector);
            return source.Where(x => selector(x).Equals(min));
        }
        public static IEnumerable<TSource> WhereMin<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            var min = source.Min(selector);
            return source.Where(x => selector(x).Equals(min));
        }
        public static IEnumerable<TSource> WhereMin<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            var min = source.Min(selector);
            return source.Where(x => selector(x).Equals(min));
        }
        public static IEnumerable<TSource> WhereMin<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            var min = source.Min(selector);
            return source.Where(x => selector(x).Equals(min));
        }
        public static IEnumerable<TSource> WhereMin<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            var min = source.Min(selector);
            return source.Where(x => selector(x).Equals(min));
        }
        public static IEnumerable<TSource> WhereMin<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var min = source.Min(selector);
            return source.Where(x => selector(x).Equals(min));
        }
    }
}
