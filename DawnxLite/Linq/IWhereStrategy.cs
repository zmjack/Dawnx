using System;
using System.Linq.Expressions;

namespace Dawnx.Linq
{
    public interface IWhereStrategy<TEntity>
    {
        Expression<Func<TEntity, bool>> StrategyExpression { get; }
    }

}
