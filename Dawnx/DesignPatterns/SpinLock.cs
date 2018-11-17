using System;

namespace Dawnx.DesignPatterns
{
    public static class SpinLock
    {
        /// <summary>
        /// Do a task with SpinLock pattern:
        ///     do(@task) until(@until)
        /// </summary>
        /// <param name="until"></param>
        /// <param name="task"></param>
        public static void Do(Action task, Func<bool> until)
        {
            do { task(); }
            while (!until());
        }

    }
}
