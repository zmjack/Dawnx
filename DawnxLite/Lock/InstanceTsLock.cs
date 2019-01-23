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
        public override string InternString(TInstance instance)
        {
            return string.Intern(
                Thread.CurrentThread.ManagedThreadId.ToString() + "\0"
                + typeof(TInstance).FullName + "\0"
                + FlagLambdas.Select(x => x(instance).ToString()).Join("\0"));
        }
    }
}
