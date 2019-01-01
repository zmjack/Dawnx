using Dawnx.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIQueryable
    {
        public static IOrderedQueryable<TEntity> OrderByCase<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, string>> memberExp,
            string[] orderValues)
            => @this.OrderByCaseStrategy(new OrderByCaseStrategy<TEntity>(memberExp, orderValues));
        
        public static IOrderedQueryable<TEntity> OrderByCaseDescending<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, string>> memberExp,
            string[] orderValues)
            => @this.OrderByCaseDescendingStrategy(new OrderByCaseStrategy<TEntity>(memberExp, orderValues));

    }
}
