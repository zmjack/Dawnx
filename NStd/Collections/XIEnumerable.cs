using System.Collections.Generic;
using System.ComponentModel;

namespace System.Collections
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

    }
}
