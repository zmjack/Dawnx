using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIQueryable
    {
        public static int MinOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector, int @default = default(int))
            => source.Any() ? source.Min(selector) : @default;
        public static long MinOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector, long @default = default(long))
            => source.Any() ? source.Min(selector) : @default;
        public static float MinOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector, float @default = default(float))
            => source.Any() ? source.Min(selector) : @default;
        public static double MinOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector, double @default = default(double))
            => source.Any() ? source.Min(selector) : @default;
        public static decimal MinOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, decimal @default = default(decimal))
            => source.Any() ? source.Min(selector) : @default;
        public static TResult MinOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, TResult @default = default(TResult))
            => source.Any() ? source.Min(selector) : @default;

        public static int MinOrDefault(this IQueryable<int> source, int @default = default(int))
            => source.Any() ? source.Min() : @default;
        public static long MinOrDefault(this IQueryable<long> source, long @default = default(long))
            => source.Any() ? source.Min() : @default;
        public static float MinOrDefault(this IQueryable<float> source, float @default = default(float))
            => source.Any() ? source.Min() : @default;
        public static double MinOrDefault(this IQueryable<double> source, double @default = default(double))
            => source.Any() ? source.Min() : @default;
        public static decimal MinOrDefault(this IQueryable<decimal> source, decimal @default = default(decimal))
            => source.Any() ? source.Min() : @default;
        public static TSource MinOrDefault<TSource>(this IQueryable<TSource> source, TSource @default = default(TSource))
            => source.Any() ? source.Min() : @default;
    }

}
