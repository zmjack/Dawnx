using System;
using System.Threading;

namespace Dawnx.Lock
{
    /// <summary>
    /// Type thread safe lock, inherits from <see cref="InstanceLock{TInstance}" />.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public static class TypeTsLock<TType>
    {
        public static string InternString => string.Intern(
            $"<{Thread.CurrentThread.ManagedThreadId.ToString()}> " +
            $"{typeof(TType).FullName}");

        public static Lock Begin(TimeSpan timeout) => Lock.Begin(InternString, timeout);
        public static Lock Begin(int millisecondsTimeout) => Lock.Begin(InternString, millisecondsTimeout);
    }
}
