using Dawnx.Reflection;
using Dawnx.Utilities;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Dawnx.Linq
{
    public class WhereBetweenStrategy<TEntity> : IWhereStrategy<TEntity>
    {
        public Expression<Func<TEntity, bool>> StrategyExpression { get; }

        private static MethodInfo _Method_op_LessThan
            = typeof(DateTime).GetMethodViaQualifiedName("Boolean op_LessThan(System.DateTime, System.DateTime)");

        public WhereBetweenStrategy(
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> startExp,
            Expression<Func<TEntity, DateTime>> endExp)
        {
            StrategyExpression = Expression.Lambda<Func<TEntity, bool>>(
                Expression.AndAlso(
                    Expression.LessThanOrEqual(
                        startExp.Body.RebindParameter(startExp.Parameters[0], memberExp.Parameters[0]),
                        memberExp.Body,
                        false, _Method_op_LessThan),
                    Expression.LessThanOrEqual(
                        memberExp.Body,
                        endExp.Body.RebindParameter(endExp.Parameters[0], memberExp.Parameters[0]),
                        false, _Method_op_LessThan)), memberExp.Parameters);
        }

        public WhereBetweenStrategy(
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime start,
            Expression<Func<TEntity, DateTime>> endExp)
        {
            StrategyExpression = Expression.Lambda<Func<TEntity, bool>>(
                Expression.AndAlso(
                    Expression.LessThanOrEqual(
                        Expression.Constant(start),
                        memberExp.Body,
                        false, _Method_op_LessThan),
                    Expression.LessThanOrEqual(
                        memberExp.Body,
                        endExp.Body.RebindParameter(endExp.Parameters[0], memberExp.Parameters[0]),
                        false, _Method_op_LessThan)), memberExp.Parameters);
        }

        public WhereBetweenStrategy(
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> startExp,
            DateTime end)
        {
            StrategyExpression = Expression.Lambda<Func<TEntity, bool>>(
                Expression.AndAlso(
                    Expression.LessThanOrEqual(
                        startExp.Body.RebindParameter(startExp.Parameters[0], memberExp.Parameters[0]),
                        memberExp.Body,
                        false, _Method_op_LessThan),
                    Expression.LessThanOrEqual(
                        memberExp.Body,
                        Expression.Constant(end),
                        false, _Method_op_LessThan)), memberExp.Parameters);
        }

        public WhereBetweenStrategy(
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime start,
            DateTime end)
        {
            StrategyExpression = Expression.Lambda<Func<TEntity, bool>>(
                Expression.AndAlso(
                    Expression.LessThanOrEqual(
                        Expression.Constant(start),
                        memberExp.Body,
                        false, _Method_op_LessThan),
                    Expression.LessThanOrEqual(
                        memberExp.Body,
                        Expression.Constant(end),
                        false, _Method_op_LessThan)), memberExp.Parameters);
        }
    }
}
