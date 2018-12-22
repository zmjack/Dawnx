using System;

namespace Dawnx.Patterns
{
    public static class UseDoubleCheck
    {
        /// <summary>
        /// Do a task with Double-checked locking pattern:
        ///     if(@if) -> lock(@locker) -> if(@if) -> do(@then).
        /// </summary>
        /// <param name="if"></param>
        /// <param name="lock"></param>
        /// <param name="then"></param>
        public static void Do(Func<bool> @if, object @lock, Action then)
        {
            if (@if())
                lock (@lock)
                    if (@if())
                        then();
        }

    }
}
