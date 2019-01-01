using Dawnx.Reflection;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Dawnx.Linq
{
    public class WhereBeforeStrategy<TEntity> : IWhereStrategy<TEntity>
    {
        public Expression<Func<TEntity, bool>> StrategyExpression { get; }

        private static MethodInfo _Method_op_LessThanOrEqual
            = typeof(DateTime).GetMethodViaQualifiedName("Boolean op_LessThanOrEqual(System.DateTime, System.DateTime)");

        public WhereBeforeStrategy(
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> beforeExp,
            bool includePoint)
        {
            var left = memberExp.Body;
            var right = beforeExp.Body.RebindParameter(beforeExp.Parameters[0], memberExp.Parameters[0]);

            StrategyExpression = Expression.Lambda<Func<TEntity, bool>>(includePoint ?
                Expression.LessThanOrEqual(left, right, false, _Method_op_LessThanOrEqual)
                : Expression.LessThan(left, right, false, _Method_op_LessThanOrEqual), memberExp.Parameters);
        }

        public WhereBeforeStrategy(
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime before,
            bool includePoint)
        {
            var left = memberExp.Body;
            var right = Expression.Constant(before);

            StrategyExpression = Expression.Lambda<Func<TEntity, bool>>(includePoint ?
                Expression.LessThanOrEqual(left, right, true, _Method_op_LessThanOrEqual)
                : Expression.LessThan(left, right, true, _Method_op_LessThanOrEqual), memberExp.Parameters);
        }
    }
}
