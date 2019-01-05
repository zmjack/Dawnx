using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        public static IEnumerable<TSource> WhereMinOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, int>> selector, int @default = default(int))
        {
            var min = source.MinOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min, typeof(int))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMinOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, long>> selector, long @default = default(long))
        {
            var min = source.MinOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min, typeof(long))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMinOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, float>> selector, float @default = default(float))
        {
            var min = source.MinOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min, typeof(float))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMinOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, double>> selector, double @default = default(double))
        {
            var min = source.MinOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min, typeof(double))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMinOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, decimal>> selector, decimal @default = default(decimal))
        {
            var min = source.MinOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min, typeof(decimal))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMinOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, int?>> selector, int? @default = default(int?))
        {
            var min = source.MinOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min, typeof(int?))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMinOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, long?>> selector, long? @default = default(long?))
        {
            var min = source.MinOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min, typeof(long?))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMinOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, float?>> selector, float? @default = default(float?))
        {
            var min = source.MinOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min, typeof(float?))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMinOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, double?>> selector, double? @default = default(double?))
        {
            var min = source.MinOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min, typeof(double?))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMinOrDefault<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, decimal?>> selector, decimal? @default = default(decimal?))
        {
            var min = source.MinOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min, typeof(decimal?))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Expression<Func<TSource, TResult>> selector, TResult @default = default(TResult))
        {
            var min = source.MinOrDefault(selector.Compile(), @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min, typeof(TResult))), selector.Parameters);
            return source.Where(whereExp.Compile());
        }

    }
}
