using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace Dawnx.Lock
{
    public class InstanceTsLock<TInstance> : InstanceLock<TInstance>
    {
        public InstanceTsLock(params Expression<Func<TInstance, object>>[] flagExpressions) : base(flagExpressions)
        {
        }

        public override string InternString(TInstance instance)
        {
            return string.Intern(
                Thread.CurrentThread.ManagedThreadId.ToString() + "\0"
                + typeof(TInstance).FullName + "\0"
                + FlagLambdas.Select(x => x(instance).ToString()).Join("\0"));
        }
    }
}
