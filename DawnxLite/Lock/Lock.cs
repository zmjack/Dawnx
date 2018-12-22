using System;
using System.Threading;

namespace Dawnx.Lock
{
    public sealed class Lock : IDisposable
    {
        public string InternString { get; }
        
        internal static Lock Get(string internString, TimeSpan timeout) => new Lock(internString, (int)timeout.TotalMilliseconds);
        internal static Lock Get(string internString, int millisecondsTimeout) => new Lock(internString, millisecondsTimeout);

        private Lock(string internString, int millisecondsTimeout)
        {
            InternString = internString;

            if (!Monitor.TryEnter(internString, millisecondsTimeout))
                throw new SynchronizationLockException();
        }

        public void Dispose()
        {
            Monitor.Exit(InternString);
        }
    }
}
