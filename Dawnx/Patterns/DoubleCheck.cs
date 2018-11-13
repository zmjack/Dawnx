using System;

namespace Dawnx.Patterns
{
    /// <summary>
    /// Double-checked locking pattern: if(condition) -> lock(locker) -> if(condition) -> do(task).
    /// </summary>
    public static class DoubleCheck
    {
        /// <summary>
        /// Do task with Double-checked locking pattern.
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
        /// Do task with Double-checked locking pattern.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="locker"></param>
        /// <param name="condition"></param>
        /// <param name="task"></param>
        public static void Do<T>(T locker, Func<bool> condition, Action task)
        {
            if (condition())
                lock (locker)
                    if (condition())
                        task();
        }

    }
}
