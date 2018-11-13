using System;

namespace Dawnx.Patterns
{
    public static class DoubleCheck
    {
        /// <summary>
        /// Do a task with Double-checked locking pattern:
        ///     if(condition) -> lock(locker) -> if(condition) -> do(task).
        /// </summary>
        /// <param name="locker"></param>
        /// <param name="condition"></param>
        /// <param name="task"></param>
        public static void Do(string locker, Func<bool> condition, Action task)
        {
            if (condition())
                lock (locker)
                    if (condition())
                        task();
        }

        /// <summary>
        /// Do a task with Double-checked locking pattern:
        ///     if(condition) -> lock(locker) -> if(condition) -> do(task).
        /// </summary>
        /// <typeparam name="TLocker"></typeparam>
        /// <param name="locker"></param>
        /// <param name="condition"></param>
        /// <param name="task"></param>
        public static void Do<TLocker>(TLocker locker, Func<bool> condition, Action task)
        {
            if (condition())
                lock (locker)
                    if (condition())
                        task();
        }

    }
}
