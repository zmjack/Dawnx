using System;

namespace Dawnx.Patterns
{
    /// <summary>
    /// Double-checked locking pattern: if -> lock -> if -> dosth.
    /// </summary>
    public static class DoubleCheck
    {
        public static void Do(string lockTarget, Func<bool> checkCondition, Action task)
        {
            if (checkCondition())
                lock (lockTarget)
                    if (checkCondition())
                        task();
        }
        
        public static void Do<T>(ref T lockObj, Func<bool> checkCondition, Action task)
        {
            if (checkCondition())
                lock (lockObj)
                    if (checkCondition())
                        task();
        }

    }
}
