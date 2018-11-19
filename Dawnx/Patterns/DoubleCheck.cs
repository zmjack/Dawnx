using System;

namespace Dawnx.Patterns
{
    public static class UseDoubleCheck
    {
        /// <summary>
        /// Do a task with Double-checked locking pattern:
        ///     if(@if) -> lock(@locker) -> if(@if) -> do(@then).
        /// </summary>
        /// <param name="locker"></param>
        /// <param name="@if"></param>
        /// <param name="then"></param>
        public static void Do(string locker, Func<bool> @if, Action then)
        {
            if (@if())
                lock (locker)
                    if (@if())
                        then();
        }

        /// <summary>
        /// Do a task with Double-checked locking pattern:
        ///     if(@if) -> lock(@locker) -> if(@if) -> do(@then).
        /// </summary>
        /// <typeparam name="TLocker"></typeparam>
        /// <param name="locker"></param>
        /// <param name="if"></param>
        /// <param name="then"></param>
        public static void Do<TLocker>(TLocker locker, Func<bool> @if, Action then)
        {
            if (@if())
                lock (locker)
                    if (@if())
                        then();
        }

    }
}
