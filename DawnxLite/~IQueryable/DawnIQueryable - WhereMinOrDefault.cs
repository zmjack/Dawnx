using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIQueryable
    {
        public static IQueryable<TSource> WhereMinOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, TResult @default = default(TResult))
        {
            var min = source.MinOrDefault(selector, @default);
            var whereExp = Expression.Lambda<Func<TSource, bool>>(
                Expression.Equal(selector.Body, Expression.Constant(min, typeof(TResult))), selector.Parameters);
            return source.Where(whereExp);
        }

    }
}
