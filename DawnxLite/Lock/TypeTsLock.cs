using System;
using System.Threading;

namespace Dawnx.Lock
{
    public static class TypeTsLock<TType>
    {
        public static string InternString => string.Intern(
            Thread.CurrentThread.ManagedThreadId.ToString() + "\0"
            + typeof(TType).FullName);
        
        public static Lock Begin(TimeSpan timeout) => Lock.Get(InternString, timeout);
        public static Lock Begin(int millisecondsTimeout) => Lock.Get(InternString, millisecondsTimeout);
    }
}
