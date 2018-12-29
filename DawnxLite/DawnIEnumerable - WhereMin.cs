using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        public static IEnumerable<TSource> WhereMin<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, int>> selector)
        {
            var min = source.Min(selector.Compile());
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min)), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMin<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, long>> selector)
        {
            var min = source.Min(selector.Compile());
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min)), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMin<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, float>> selector)
        {
            var min = source.Min(selector.Compile());
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min)), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMin<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, double>> selector)
        {
            var min = source.Min(selector.Compile());
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min)), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMin<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, decimal>> selector)
        {
            var min = source.Min(selector.Compile());
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min)), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMin<TSource, TResult>(this IEnumerable<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            var min = source.Min(selector.Compile());
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min)), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
    }
}
