using Dawnx.Utilities;
using NStandard;
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
        public string LockName { get; }
        public Expression<Func<TInstance, object>>[] Flags { get; }
        protected Func<TInstance, object>[] FlagLambdas { get; }

        protected InstanceLock(params Expression<Func<TInstance, object>>[] flags)
        {
            var isAllExpressionValid = flags.All(x =>
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

            Flags = flags;
            FlagLambdas = Flags.Select(x => x.Compile()).ToArray();
        }

        protected InstanceLock(string lockName, params Expression<Func<TInstance, object>>[] flags)
            : this(flags)
        {
            LockName = lockName;
        }

        public static InstanceLock<TInstance> Get(params Expression<Func<TInstance, object>>[] flags)
            => new InstanceLock<TInstance>(flags);
        public static InstanceLock<TInstance> Get(string lockName, params Expression<Func<TInstance, object>>[] flags)
            => new InstanceLock<TInstance>(lockName, flags);

        public virtual string InternString(TInstance instance)
        {
            return string.Intern(
                $"{typeof(TInstance).FullName} " +
                $"{FlagLambdas.Select(x => x(instance).ToString().Flow(StringFlows.UrlEncode)).Join(" ")} " +
                $"({LockName})");
        }

        public Lock Begin(TInstance instance, TimeSpan timeout) => Lock.Begin(InternString(instance), timeout);
        public Lock Begin(TInstance instance, int millisecondsTimeout) => Lock.Begin(InternString(instance), millisecondsTimeout);
    }

}
