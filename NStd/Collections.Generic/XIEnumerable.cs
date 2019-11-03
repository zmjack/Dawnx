using System.ComponentModel;

namespace System.Collections.Generic
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XIEnumerable
    {
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
