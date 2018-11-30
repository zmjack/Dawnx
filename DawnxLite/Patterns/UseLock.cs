using System;
using System.Threading;

namespace Dawnx.Patterns
{
    public static class UseLock
    {
        public static bool TryDo(object lockObj, TimeSpan timeout, Action task)
        {
            if (Monitor.TryEnter(lockObj, timeout))
            {
                try
                {
                    task();
                    return true;
                }
                finally { Monitor.Exit(lockObj); }
            }
            else return false;
        }

        public static bool TryDo(object lockObj, Action task)
        {
            if (Monitor.TryEnter(lockObj))
            {
                try
                {
                    task();
                    return true;
                }
                finally { Monitor.Exit(lockObj); }
            }
            else return false;
        }

    }
}
