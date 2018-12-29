using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        public static IEnumerable<TSource> WhereMax<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, int>> selector)
        {
            var max = source.Max(selector.Compile());
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max)), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMax<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, long>> selector)
        {
            var max = source.Max(selector.Compile());
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max)), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMax<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, float>> selector)
        {
            var max = source.Max(selector.Compile());
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max)), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMax<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, double>> selector)
        {
            var max = source.Max(selector.Compile());
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max)), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMax<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, decimal>> selector)
        {
            var max = source.Max(selector.Compile());
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max)), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
        public static IEnumerable<TSource> WhereMax<TSource, TResult>(this IEnumerable<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            var max = source.Max(selector.Compile());
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max)), selector.Parameters);
            return source.Where(whereExp.Compile());
        }
    }
}
