using Dawnx.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        public static IOrderedEnumerable<TEntity> OrderByCase<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, string>> memberExp,
            string[] orderValues)
            => @this.OrderByCaseStrategy(new OrderByCaseStrategy<TEntity>(memberExp, orderValues));
        
        public static IOrderedEnumerable<TEntity> OrderByCaseDescending<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, string>> memberExp,
            string[] orderValues)
            => @this.OrderByCaseDescendingStrategy(new OrderByCaseStrategy<TEntity>(memberExp, orderValues));

    }
}
