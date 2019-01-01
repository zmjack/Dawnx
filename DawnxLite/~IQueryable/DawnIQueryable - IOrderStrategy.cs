using Dawnx.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIQueryable
    {
        /// <summary>
        /// Use an OrderByStrategy to generate an orberby expression.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="this"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> OrderByCaseStrategy<TEntity>(this IQueryable<TEntity> @this, IOrderStrategy<TEntity> strategy)
            => @this.OrderBy(strategy.StrategyExpression);

        /// <summary>
        /// Use an OrderByStrategy to generate an orberby expression.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="this"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> OrderByCaseDescendingStrategy<TEntity>(this IQueryable<TEntity> @this, IOrderStrategy<TEntity> strategy)
            => @this.OrderByDescending(strategy.StrategyExpression);
        
    }

}
