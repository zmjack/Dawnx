using System;
using System.Linq.Expressions;

namespace Linqx.Strategies
{
    public interface IOrderStrategy<TEntity>
    {
        Expression<Func<TEntity, int>> StrategyExpression { get; }
    }

}
