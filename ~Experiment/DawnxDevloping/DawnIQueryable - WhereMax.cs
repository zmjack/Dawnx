using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DawnxDevloping
{
    public static partial class DawnIQueryable
    {
        public static IQueryable<TSource> WhereMax<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
        {
            var max = source.Max(selector);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max)), selector.Parameters);
            return source.Where(whereExp);
        }
        public static IQueryable<TSource> WhereMax<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
        {
            var max = source.Max(selector);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max)), selector.Parameters);
            return source.Where(whereExp);
        }
        public static IQueryable<TSource> WhereMax<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
        {
            var max = source.Max(selector);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max)), selector.Parameters);
            return source.Where(whereExp);
        }
        public static IQueryable<TSource> WhereMax<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
        {
            var max = source.Max(selector);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max)), selector.Parameters);
            return source.Where(whereExp);
        }
        public static IQueryable<TSource> WhereMax<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
        {
            var max = source.Max(selector);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max)), selector.Parameters);
            return source.Where(whereExp);
        }
        public static IQueryable<TSource> WhereMax<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            var max = source.Max(selector);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(max)), selector.Parameters);
            return source.Where(whereExp);
        }
    }
}
