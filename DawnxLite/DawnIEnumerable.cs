using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        /// <summary>
        /// Do action for each item.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Each<TSource>(this IEnumerable<TSource> @this, Action<TSource> task)
        {
            foreach (var item in @this)
                task(item);
            return @this;
        }

        /// <summary>
        /// Do action for each item.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable Each<TSource>(IEnumerable @this, Action<TSource> task)
        {
            foreach (TSource item in @this)
                task(item);
            return @this;
        }

        /// <summary>
        /// Do action for each item.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Each<TSource>(this IEnumerable<TSource> @this, Action<TSource, int> task)
        {
            int i = 0;
            foreach (var item in @this)
                task(item, i++);
            return @this;
        }

        /// <summary>
        /// Do action for each item.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable Each<TSource>(this IEnumerable @this, Action<TSource, int> task)
        {
            int i = 0;
            foreach (TSource item in @this)
                task(item, i++);
            return @this;
        }

        public static IEnumerable<TSource> WhereMin<TSource, TResult>(this IEnumerable<TSource> @this, Func<TSource, TResult> selector)
        {
            var min = @this.Min(selector);
            return @this.Where(x => selector(x).Equals(min));
        }

        public static IEnumerable<TSource> WhereMax<TSource, TResult>(this IEnumerable<TSource> @this, Func<TSource, TResult> selector)
        {
            var max = @this.Max(selector);
            return @this.Where(x => selector(x).Equals(max));
        }

        /// <summary>
        /// Returns a collection of tuples containing values and indexes.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<VI<TSource>> AsVI<TSource>(this IEnumerable<TSource> @this)
        {
            int i = 0;
            foreach (var item in @this)
                yield return new VI<TSource>(item, i++);
        }

        /// <summary>
        /// Returns a collection of tuples containing values and indexes.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<VI<TSource>> AsVI<TSource>(this IEnumerable @this)
        {
            int i = 0;
            foreach (TSource item in @this)
                yield return new VI<TSource>(item, i++);
        }

        /// <summary>
        /// Concatenates the members of a collection, using the specified separator between each member.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join<TSource>(this IEnumerable<TSource> @this, string separator) => string.Join(separator, @this);

        /// <summary>
        /// Projects each element of the specified array into a new distributed array collection by 'Classify' method.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TConditionRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="Classify"></param>
        /// <returns></returns>
        public static IEnumerable<TSource[]> Distribute<TSource, TConditionRet>(
            this IEnumerable<TSource> @this,
            Func<TSource, TConditionRet> Classify)
        {
            if (@this.Any())
            {
                var value = Classify(@this.First());
                var group = new List<TSource>();

                foreach (var item in @this)
                {
                    var itemValue = Classify(item);

                    if (!itemValue.Equals(value))
                    {
                        value = itemValue;
                        yield return group.ToArray();
                        group.Clear();
                    }

                    group.Add(item);
                }
                yield return group.ToArray();
            }
            else yield return new TSource[0];
        }

        /// <summary>
        /// Projects each element of the specified array into a new distributed array collection by 'Classify' method.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TConditionRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="Classify"></param>
        /// <returns></returns>
        public static IEnumerable<TSource[]> Distribute<TSource, TConditionRet>(
            this IEnumerable<TSource> @this,
            Func<TSource, int, TConditionRet> Classify)
        {
            if (@this.Any())
            {
                int index = 0;
                var value = Classify(@this.First(), 0);
                var group = new List<TSource>();

                foreach (var item in @this)
                {
                    var itemValue = Classify(item, index++);

                    if (!itemValue.Equals(value))
                    {
                        value = itemValue;
                        yield return group.ToArray();
                        group.Clear();
                    }

                    group.Add(item);
                }
                yield return group.ToArray();
            }
            else yield return new TSource[0];
        }

        /// <summary>
        /// Projects each element of the specified array into a new distributed array collection by its index.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<TSource[]> Distribute<TSource>(
            this IEnumerable<TSource> @this, int count)
        {
            if (@this.Any())
            {
                var index = 0;
                var group = new List<TSource>();

                foreach (var item in @this)
                {
                    if (index > 0 && (index % count) == 0)
                    {
                        yield return group.ToArray();
                        group.Clear();
                    }

                    group.Add(item);
                    index++;
                }
                yield return group.ToArray();
            }
            else yield return new TSource[0];
        }

    }
}
