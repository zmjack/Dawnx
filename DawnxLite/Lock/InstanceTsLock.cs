using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace Dawnx.Lock
{
    /// <summary>
    /// Instance thread safe lock, inherits from <see cref="InstanceLock{TInstance}" />.
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    public class InstanceTsLock<TInstance> : InstanceLock<TInstance>
    {
        protected InstanceTsLock(params Expression<Func<TInstance, object>>[] flagExpressions) : base(flagExpressions) { }
        protected InstanceTsLock(string identifier, params Expression<Func<TInstance, object>>[] flagExpressions)
            : base(identifier, flagExpressions) { }

        public override string InternString(TInstance instance)
        {
            return string.Intern(
                $"<{Thread.CurrentThread.ManagedThreadId.ToString()}> " +
                $"{typeof(TInstance).FullName} " +
                $"{FlagLambdas.Select(x => x(instance).ToString()).Join(" ")} " +
                $"({Identifier})");
        }

        public static InstanceTsLock<TInstance> Get(params Expression<Func<TInstance, object>>[] flagExpressions)
            => new InstanceTsLock<TInstance>(flagExpressions);
        public static InstanceTsLock<TInstance> Get(string identifier, params Expression<Func<TInstance, object>>[] flagExpressions)
            => new InstanceTsLock<TInstance>(identifier, flagExpressions);
    }
}
