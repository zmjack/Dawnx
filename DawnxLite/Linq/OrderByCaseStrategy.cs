using Dawnx.Reflection;
using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Dawnx.Linq
{
    public class OrderByCaseStrategy<TEntity> : IOrderStrategy<TEntity>
    {
        public Expression<Func<TEntity, int>> StrategyExpression { get; }

        private static MethodInfo _Method_op_LessThan
            = typeof(DateTime).GetMethodViaFormatName("Boolean op_LessThan(System.DateTime, System.DateTime)");

        public OrderByCaseStrategy(
            Expression<Func<TEntity, string>> memberExp,
            string[] orderValues)
        {
            var valueLenth = orderValues.Length;
            Expression conditionExp = null;

            foreach (var vi in orderValues.Reverse().AsVI())
            {
                var compareExp = Expression.Equal(memberExp.Body, Expression.Constant(vi.Value));
                if (conditionExp is null)
                {
                    conditionExp =
                        Expression.Condition(
                            compareExp,
                            Expression.Constant(valueLenth - 1 - vi.Index),
                            Expression.Constant(valueLenth));
                }
                else
                {
                    conditionExp =
                        Expression.Condition(
                            compareExp,
                            Expression.Constant(valueLenth - 1 - vi.Index),
                            conditionExp);
                }
            }

            var quote = Expression.Quote(
                        Expression.Lambda<Func<TEntity, int>>(conditionExp, memberExp.Parameters));

            StrategyExpression =
                Expression.Lambda<Func<TEntity, int>>(conditionExp, memberExp.Parameters);
        }

    }
}
