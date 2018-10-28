using System;
using System.Linq.Expressions;

namespace Dawnx.Entity
{
    public interface IWhereStrategy<TEntity>
    {
        Expression<Func<TEntity, bool>> StrategyExpression { get; }
    }

}
