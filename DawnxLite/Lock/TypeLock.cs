using NStandard;
using NStandard.Flows;
using System;

namespace Dawnx.Lock
{
    /// <summary>
    /// Type lock
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class TypeLock<TType> : TypeLock
    {
        public string[] Flags { get; }

        protected TypeLock(string lockName) : base(typeof(TType), lockName) { }

        public static TypeLock<TType> Get(string lockName) => new TypeLock<TType>(lockName);
    }

    public class TypeLock
    {
        public Type Type { get; }
        public string LockName { get; }

        protected TypeLock(Type type, string lockName)
        {
            Type = type;
            LockName = lockName;
        }

        public static TypeLock Get(Type type, string lockName) => new TypeLock(type, lockName);

        public virtual string InternString
        {
            get
            {
                return string.Intern($"{Type.FullName} {LockName.Flow(StringFlow.UrlEncode)}");
            }
        }

        public Lock Begin(TimeSpan timeout) => Lock.Begin(InternString, timeout);
        public Lock Begin(int millisecondsTimeout) => Lock.Begin(InternString, millisecondsTimeout);
    }

}
