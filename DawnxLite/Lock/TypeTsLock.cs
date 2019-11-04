using NStandard;
using NStandard.Flows;
using System;
using System.Threading;

namespace Dawnx.Lock
{
    /// <summary>
    /// Type thread safe lock, inherits from <see cref="InstanceLock{TInstance}" />.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class TypeTsLock<TType> : TypeTsLock
    {
        protected TypeTsLock(string lockName) : base(typeof(TType), lockName) { }

        public static TypeTsLock<TType> Get(string lockName) => new TypeTsLock<TType>(lockName);
    }

    public class TypeTsLock : TypeLock
    {
        protected TypeTsLock(Type type, string lockName) : base(type, lockName) { }

        public new static TypeTsLock Get(Type type, string lockName) => new TypeTsLock(type, lockName);

        public override string InternString
        {
            get
            {
                return string.Intern(
                    $"<{Thread.CurrentThread.ManagedThreadId.ToString()}> " +
                    $"{Type.FullName} {LockName.Flow(StringFlow.UrlEncode)}");
            }
        }
    }

}
