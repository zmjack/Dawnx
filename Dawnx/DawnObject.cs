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

        /// <summary>
        /// If the element is null, the method will be called. Otherwise, nothing will happen.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="method"></param>
        public static TSelf IfNull<TSelf>(this TSelf @this, Action<TSelf> method)
        {
            if (@this == null)
                method(@this);

            return @this;
        }

        /// <summary>
        /// If the element is not null, the method will be called. Otherwise, nothing will happen.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="method"></param>
        public static TSelf IfNotNull<TSelf>(this TSelf @this, Action<TSelf> method)
        {
            if (@this != null)
                method(@this);

            return @this;
        }

        /// <summary>
        /// If the specified condition is true, the method will be called. Otherwise, nothing will happen.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="method"></param>
        public static TSelf If<TSelf>(this TSelf @this, Predicate<TSelf> condition, Action<TSelf> method)
        {
            if (condition(@this))
                method(@this);

            return @this;
        }

        /// <summary>
        /// This function is equal to '@this as T'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T As<T>(this object @this) where T : class => @this as T;

        /// <summary>
        /// This function is equal to '(T)@this'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T CastTo<T>(this object @this) => (T)(dynamic)@this;

    }
}
