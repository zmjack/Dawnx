using System;
using System.Linq.Expressions;

namespace Linqx.Strategies
{
    public interface IWhereStrategy<TEntity>
    {
        Expression<Func<TEntity, bool>> StrategyExpression { get; }
    }

}
