using System;
using System.Threading;

namespace Dawnx.Patterns
{
    public static class UseLock
    {
        /// <summary>
        /// Do a task with Lock pattern:
        ///     if TryEnter(@lockObj, @timeout) try { @task } finally { Exit(@lockObj) }
        /// </summary>
        /// <param name="lockObj"></param>
        /// <param name="timeout"></param>
        /// <param name="task"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Do a task with Lock pattern:
        ///     if TryEnter(@lockObj) try { @task } finally { Exit(@lockObj) }
        /// </summary>
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
