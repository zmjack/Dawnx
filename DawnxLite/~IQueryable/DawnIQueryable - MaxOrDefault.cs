using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIQueryable
    {
        public static int MaxOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector, int @default = default(int))
            => source.Any() ? source.Max(selector) : @default;
        public static long MaxOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector, long @default = default(long))
            => source.Any() ? source.Max(selector) : @default;
        public static float MaxOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector, float @default = default(float))
            => source.Any() ? source.Max(selector) : @default;
        public static double MaxOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector, double @default = default(double))
            => source.Any() ? source.Max(selector) : @default;
        public static decimal MaxOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, decimal @default = default(decimal))
            => source.Any() ? source.Max(selector) : @default;
        public static TResult MaxOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, TResult @default = default(TResult))
            => source.Any() ? source.Max(selector) : @default;

        public static int MaxOrDefault(this IQueryable<int> source, int @default = default(int))
            => source.Any() ? source.Max() : @default;
        public static long MaxOrDefault(this IQueryable<long> source, long @default = default(long))
            => source.Any() ? source.Max() : @default;
        public static float MaxOrDefault(this IQueryable<float> source, float @default = default(float))
            => source.Any() ? source.Max() : @default;
        public static double MaxOrDefault(this IQueryable<double> source, double @default = default(double))
            => source.Any() ? source.Max() : @default;
        public static decimal MaxOrDefault(this IQueryable<decimal> source, decimal @default = default(decimal))
            => source.Any() ? source.Max() : @default;
        public static TSource MaxOrDefault<TSource>(this IQueryable<TSource> source, TSource @default = default(TSource))
            => source.Any() ? source.Max() : @default;
    }
}
