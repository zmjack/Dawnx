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

        /// <summary>
        /// Do a task with Double-checked locking pattern:
        ///     if(@if) -> lock(@locker) -> if(@if) -> do(@then).
        /// </summary>
        /// <param name="if"></param>
        /// <param name="lock"></param>
        /// <param name="then"></param>
        public static void Do(Func<bool> @if, string @lock, Action then)
        {
            if (@if())
                lock (@lock)
                    if (@if())
                        then();
        }

        /// <summary>
        /// Do a task with Double-checked locking pattern:
        ///     if(@if) -> lock(@locker) -> if(@if) -> do(@then).
        /// </summary>
        /// <param name="if"></param>
        /// <param name="lock"></param>
        /// <param name="then"></param>
        public static TRet Do<TRet>(Func<bool> @if, object @lock, Func<TRet> then, TRet defaultReturn = default)
        {
            if (@if())
                lock (@lock)
                    if (@if())
                        return then();
            return defaultReturn;
        }

        /// <summary>
        /// Do a task with Double-checked locking pattern:
        ///     if(@if) -> lock(@locker) -> if(@if) -> do(@then).
        /// </summary>
        /// <param name="if"></param>
        /// <param name="lock"></param>
        /// <param name="then"></param>
        public static TRet Do<TRet>(Func<bool> @if, string @lock, Action then, TRet defaultReturn = default)
        {
            if (@if())
                lock (@lock)
                    if (@if())
                        then();
            return defaultReturn;
        }

    }
}
