using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        public static IEnumerable<TSource> WhereMaxOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, int>> selector, int @default = default(int))
        {
            var max = source.MaxOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max, typeof(int))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMaxOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, long>> selector, long @default = default(long))
        {
            var max = source.MaxOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max, typeof(long))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMaxOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, float>> selector, float @default = default(float))
        {
            var max = source.MaxOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max, typeof(float))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMaxOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, double>> selector, double @default = default(double))
        {
            var max = source.MaxOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max, typeof(double))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMaxOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, decimal>> selector, decimal @default = default(decimal))
        {
            var max = source.MaxOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max, typeof(decimal))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMaxOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, int?>> selector, int? @default = default(int?))
        {
            var max = source.MaxOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max, typeof(int?))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMaxOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, long?>> selector, long? @default = default(long?))
        {
            var max = source.MaxOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max, typeof(long?))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMaxOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, float?>> selector, float? @default = default(float?))
        {
            var max = source.MaxOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max, typeof(float?))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMaxOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, double?>> selector, double? @default = default(double?))
        {
            var max = source.MaxOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max, typeof(double?))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMaxOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, decimal?>> selector, decimal? @default = default(decimal?))
        {
            var max = source.MaxOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max, typeof(decimal?))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Expression<Func<TSource, TResult>> selector, TResult @default = default(TResult))
        {
            var max = source.MaxOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max, typeof(TResult))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }

    }
}
