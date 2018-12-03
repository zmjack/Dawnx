using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dawnx
{
    public static class Locker
    {
        /// <summary>
        /// Craetes a special reference of string from the system.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strs"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Get<T>(params object[] strs)
        {
            return string.Intern($"{typeof(T).FullName}{"\0"}{strs.Join("\0")}");
        }

        /// <summary>
        /// Craetes a special reference of string from the system.
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Get(params object[] strs)
        {
            return string.Intern($"{strs.Join("\0")}");
        }

    }
}
