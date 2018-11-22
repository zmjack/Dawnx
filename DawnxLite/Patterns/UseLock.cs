using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dawnx.Patterns
{
    public static class UseLock
    {
        public static void Do(object lockObj, TimeSpan timeout, Action task)
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(lockObj, timeout, ref lockTaken);
                task();
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(lockObj);
            }
        }

    }
}
