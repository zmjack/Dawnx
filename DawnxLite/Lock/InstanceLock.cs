using Dawnx.Utilities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx.Lock
{
    /// <summary>
    /// Instance Lock
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    public class InstanceLock<TInstance>
    {
        public Expression<Func<TInstance, object>>[] FlagExpressions { get; }
        protected Func<TInstance, object>[] FlagLambdas { get; }

        protected InstanceLock(params Expression<Func<TInstance, object>>[] flagExpressions)
        {
            var isAllExpressionValid = flagExpressions.All(x =>
            {
                switch (x.Body.NodeType)
                {
                    case ExpressionType.Convert:
                        return BasicTypeUtility.IsBasicType((x.Body as UnaryExpression).Operand.Type);
                    case ExpressionType.MemberAccess:
                        return BasicTypeUtility.IsBasicType(x.Body.Type);
                    case ExpressionType.Constant:
                        return BasicTypeUtility.IsBasicType((x.Body as ConstantExpression).Type);
                    default: return false;
                }
            });

            if (!isAllExpressionValid)
                throw new ArgumentException("Every expression's return type must be basic type.");

            FlagExpressions = flagExpressions;
            FlagLambdas = FlagExpressions.Select(x => x.Compile()).ToArray();
        }

        public static InstanceLock<TInstance> Get(params Expression<Func<TInstance, object>>[] flagExpressions)
            => new InstanceLock<TInstance>(flagExpressions);

        public virtual string InternString(TInstance instance)
        {
            return string.Intern(
                typeof(TInstance).FullName + "\0"
                + FlagLambdas.Select(x => x(instance)?.ToString() ?? "").Join("\0"));
        }

        public Lock Begin(TInstance instance, TimeSpan timeout) => Lock.Get(InternString(instance), timeout);
        public Lock Begin(TInstance instance, int millisecondsTimeout) => Lock.Get(InternString(instance), millisecondsTimeout);
    }
}
