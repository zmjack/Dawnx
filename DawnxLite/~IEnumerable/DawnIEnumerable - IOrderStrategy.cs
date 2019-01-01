using Dawnx.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        /// <summary>
        /// Use an OrderByStrategy to generate an orberby expression.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="this"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<TEntity> OrderByCaseStrategy<TEntity>(this IEnumerable<TEntity> @this, IOrderStrategy<TEntity> strategy)
            => @this.OrderBy(strategy.StrategyExpression.Compile());

        /// <summary>
        /// Use an OrderByStrategy to generate an orberby expression.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="this"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<TEntity> OrderByCaseDescendingStrategy<TEntity>(this IEnumerable<TEntity> @this, IOrderStrategy<TEntity> strategy)
            => @this.OrderByDescending(strategy.StrategyExpression.Compile());
        
    }
}
