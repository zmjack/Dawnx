using System;

namespace Dawnx.Lock
{
    /// <summary>
    /// Type lock
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public static class TypeLock<TType>
    {
        public static readonly string InternString = string.Intern(typeof(TType).FullName);

        public static Lock Begin(TimeSpan timeout) => Lock.Get(InternString, timeout);
        public static Lock Begin(int millisecondsTimeout) => Lock.Get(InternString, millisecondsTimeout);
    }
}
