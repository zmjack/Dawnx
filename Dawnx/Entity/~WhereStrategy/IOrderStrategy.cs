using System;
using System.Linq.Expressions;

namespace Dawnx.Entity
{
    public interface IOrderStrategy<TEntity>
    {
        Expression<Func<TEntity, int>> StrategyExpression { get; }
    }

}
