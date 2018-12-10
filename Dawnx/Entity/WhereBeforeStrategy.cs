using Dawnx.Reflection;
using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Dawnx.Entity
{
    public class WhereBeforeStrategy<TEntity> : IWhereStrategy<TEntity>
    {
        public Expression<Func<TEntity, bool>> StrategyExpression { get; }

        private static MethodInfo _Method_op_LessThan
            = typeof(DateTime).GetMethodViaFormatName("Boolean op_LessThan(System.DateTime, System.DateTime)");

        public WhereBeforeStrategy(
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> beforeExp,
            bool includePoint)
        {
            var left = memberExp.Body;
            var right = ExpressionUtility.RebindParameter(beforeExp.Body, beforeExp.Parameters[0], memberExp.Parameters[0]);

            StrategyExpression = Expression.Lambda<Func<TEntity, bool>>(includePoint ?
                Expression.LessThanOrEqual(left, right, false, _Method_op_LessThan)
                : Expression.LessThan(left, right, false, _Method_op_LessThan), memberExp.Parameters);
        }
        public WhereBeforeStrategy(
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime before,
            bool includePoint)
        {
            var left = memberExp.Body;
            var right = Expression.Constant(before);

            StrategyExpression = Expression.Lambda<Func<TEntity, bool>>(includePoint ?
                Expression.LessThanOrEqual(left, right, false, _Method_op_LessThan)
                : Expression.LessThan(left, right, false, _Method_op_LessThan), memberExp.Parameters);
        }
    }
}
