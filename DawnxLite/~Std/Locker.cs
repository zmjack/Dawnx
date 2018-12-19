using System;
using System.Runtime.CompilerServices;

namespace Dawnx
{
    public static class Locker
    {
        /// <summary>
        /// Craetes a special reference of string from the system.
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Get<T>(params string[] strs)
        {
            return string.Intern($"{typeof(T).FullName}\0{strs.Join("\0")}");
        }

        /// <summary>
        /// Craetes a special reference of string from the system.
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Get(params string[] strs)
        {
            return string.Intern(strs.Join("\0"));
        }

    }
}
