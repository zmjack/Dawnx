using System;
using System.Linq.Expressions;

namespace Dawnx.Linq
{
    public interface IOrderStrategy<TEntity>
    {
        Expression<Func<TEntity, int>> StrategyExpression { get; }
    }

}
