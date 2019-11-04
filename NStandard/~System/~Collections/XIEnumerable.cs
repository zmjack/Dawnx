using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XIEnumerable
    {
        /// <summary>
        /// Returns a collection of tuples containing values and indexes.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, TSource>> AsKvPairs<TSource>(this IEnumerable @this)
        {
            int i = 0;
            foreach (TSource item in @this)
                yield return new KeyValuePair<int, TSource>(i++, item);
        }

        /// <summary>
        /// Returns a collection of KeyValuePair which contains the element's index(Key) and value.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, TSource>> AsKvPairs<TSource>(this IEnumerable<TSource> @this)
        {
            int i = 0;
            foreach (var item in @this)
                yield return new KeyValuePair<int, TSource>(i++, item);
        }

    }
}
