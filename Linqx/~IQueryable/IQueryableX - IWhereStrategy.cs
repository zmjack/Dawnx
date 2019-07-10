using Linqx.Strategies;
using System.Linq;

namespace Linqx
{
    public static partial class IQueryableX
    {
        /// <summary>
        /// Use a WhereStragtegy to generate a where expression.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="this"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereStrategy<TEntity>(this IQueryable<TEntity> @this,
            IWhereStrategy<TEntity> strategy)
            => @this.Where(strategy.StrategyExpression);

    }

}
