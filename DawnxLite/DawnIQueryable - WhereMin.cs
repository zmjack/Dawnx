using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIQueryable
    {
        public static IQueryable<TSource> WhereMin<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
        {
            var min = source.Min(selector);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min)), selector.Parameters);
            return source.Where(whereExp);
        }
        public static IQueryable<TSource> WhereMin<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
        {
            var min = source.Min(selector);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min)), selector.Parameters);
            return source.Where(whereExp);
        }
        public static IQueryable<TSource> WhereMin<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
        {
            var min = source.Min(selector);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min)), selector.Parameters);
            return source.Where(whereExp);
        }
        public static IQueryable<TSource> WhereMin<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
        {
            var min = source.Min(selector);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min)), selector.Parameters);
            return source.Where(whereExp);
        }
        public static IQueryable<TSource> WhereMin<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
        {
            var min = source.Min(selector);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min)), selector.Parameters);
            return source.Where(whereExp);
        }
        public static IQueryable<TSource> WhereMin<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            var min = source.Min(selector);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min)), selector.Parameters);
            return source.Where(whereExp);
        }
    }
}
