using Linqx.Strategies;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Linqx
{
    public static partial class IQueryableX
    {
        public static IOrderedQueryable<TEntity> OrderByCase<TEntity, TRet>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, TRet>> memberExp,
            TRet[] orderValues)
            => @this.OrderByCaseStrategy(new OrderByCaseStrategy<TEntity, TRet>(memberExp, orderValues));

        public static IOrderedQueryable<TEntity> OrderByCaseDescending<TEntity, TRet>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, TRet>> memberExp,
            TRet[] orderValues)
            => @this.OrderByCaseDescendingStrategy(new OrderByCaseStrategy<TEntity, TRet>(memberExp, orderValues));

        public static IOrderedQueryable<TEntity> ThenByCase<TEntity, TRet>(this IOrderedQueryable<TEntity> @this,
            Expression<Func<TEntity, TRet>> memberExp,
            TRet[] orderValues)
            => @this.ThenByCaseStrategy(new OrderByCaseStrategy<TEntity, TRet>(memberExp, orderValues));

        public static IOrderedQueryable<TEntity> ThenByCaseDescending<TEntity, TRet>(this IOrderedQueryable<TEntity> @this,
            Expression<Func<TEntity, TRet>> memberExp,
            TRet[] orderValues)
            => @this.ThenByCaseDescendingStrategy(new OrderByCaseStrategy<TEntity, TRet>(memberExp, orderValues));

    }
}
