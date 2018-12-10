using Dawnx.Reflection;
using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Dawnx.Entity
{
    public class WhereAfterStrategy<TEntity> : IWhereStrategy<TEntity>
    {
        public Expression<Func<TEntity, bool>> StrategyExpression { get; }

        private static MethodInfo _Method_op_LessThan
            = typeof(DateTime).GetMethodViaFormatName("Boolean op_LessThan(System.DateTime, System.DateTime)");

        public WhereAfterStrategy(
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> afterExp,
            bool includePoint)
        {
            var left = memberExp.Body;
            var right = ExpressionUtility.RebindParameter(afterExp.Body, afterExp.Parameters[0], memberExp.Parameters[0]);

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
