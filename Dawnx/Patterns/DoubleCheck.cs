using System;

namespace Dawnx.Patterns
{
    /// <summary>
    /// Double-checked locking pattern: if -> lock -> if -> dosth.
    /// </summary>
    public static class DoubleCheck
    {
        public static void Do(string locker, Func<bool> condition, Action task)
        {
            if (condition())
                lock (locker)
                    if (condition())
                        task();
        }

        public static void Do<T>(ref T locker, Func<bool> condition, Action task)
        {
            if (condition())
                lock (locker)
                    if (condition())
                        task();
        }

    }
}
