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
        public override string InternString(TInstance instance, string identifier)
        {
            return string.Intern(
                $"<{Thread.CurrentThread.ManagedThreadId.ToString()}> " +
                $"{typeof(TInstance).FullName} " +
                $"{FlagLambdas.Select(x => x(instance).ToString()).Join(" ")} " +
                $"({identifier})");
        }
    }
}
