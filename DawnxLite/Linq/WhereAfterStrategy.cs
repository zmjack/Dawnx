using Dawnx.Reflection;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Dawnx.Linq
{
    public class WhereAfterStrategy<TEntity> : IWhereStrategy<TEntity>
    {
        public Expression<Func<TEntity, bool>> StrategyExpression { get; }

        private static MethodInfo _Method_op_LessThan
            = typeof(DateTime).GetMethodViaQualifiedName("Boolean op_LessThan(System.DateTime, System.DateTime)");

        public WhereAfterStrategy(
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> afterExp,
            bool includePoint)
        {
            var left = memberExp.Body;
            var right = afterExp.Body.RebindParameter(afterExp.Parameters[0], memberExp.Parameters[0]);

            StrategyExpression = Expression.Lambda<Func<TEntity, bool>>(includePoint ?
                Expression.GreaterThanOrEqual(left, right, false, _Method_op_LessThan)
                : Expression.GreaterThan(left, right, false, _Method_op_LessThan), memberExp.Parameters);
        }

        public WhereAfterStrategy(
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime after,
            bool includePoint)
        {
            var left = memberExp.Body;
            var right = Expression.Constant(after);

            StrategyExpression = Expression.Lambda<Func<TEntity, bool>>(includePoint ?
                Expression.GreaterThanOrEqual(left, right, false, _Method_op_LessThan)
                : Expression.GreaterThan(left, right, false, _Method_op_LessThan), memberExp.Parameters);
        }
    }
}
