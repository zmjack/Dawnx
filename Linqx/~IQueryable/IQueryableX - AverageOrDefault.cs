using System;
using System.Linq;
using System.Linq.Expressions;

namespace Linqx
{
    public static partial class IQueryableX
    {
        public static double AverageOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector, double @default = default(double))
            => source.Any() ? source.Average(selector) : @default;
        public static double AverageOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector, double @default = default(double))
            => source.Any() ? source.Average(selector) : @default;
        public static float AverageOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector, float @default = default(float))
            => source.Any() ? source.Average(selector) : @default;
        public static double AverageOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector, double @default = default(double))
            => source.Any() ? source.Average(selector) : @default;
        public static decimal AverageOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, decimal @default = default(decimal))
            => source.Any() ? source.Average(selector) : @default;

        public static double AverageOrDefault(this IQueryable<int> source, double @default = default(double))
            => source.Any() ? source.Average() : @default;
        public static double AverageOrDefault(this IQueryable<long> source, double @default = default(double))
            => source.Any() ? source.Average() : @default;
        public static float AverageOrDefault(this IQueryable<float> source, float @default = default(float))
            => source.Any() ? source.Average() : @default;
        public static double AverageOrDefault(this IQueryable<double> source, double @default = default(double))
            => source.Any() ? source.Average() : @default;
        public static decimal AverageOrDefault(this IQueryable<decimal> source, decimal @default = default(decimal))
            => source.Any() ? source.Average() : @default;

    }
}
