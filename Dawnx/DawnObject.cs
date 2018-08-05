using System;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx
{
    public static class DawnObject
    {
        /// <summary>
        /// Do a task for itself.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static TSelf Self<TSelf>(this TSelf @this, Action<TSelf> task)
        {
            task(@this);
            return @this;
        }

        /// <summary>
        /// Casts the element to the specified type through the convert method.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static TRet For<TSelf, TRet>(this TSelf @this, Func<TSelf, TRet> convert)
            => convert(@this);

        /// <summary>
        /// Determines whether the specified element in a sequence by using the default equality comparer.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool In<TSource>(this TSource @this, params TSource[] sequence)
            => sequence.Contains(@this);

        /// <summary>
        /// Determines whether the specified element in a sequence by using the default equality comparer.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool In<TSource>(this TSource @this, IEnumerable<TSource> sequence)
            => sequence.Contains(@this);

    }
}
