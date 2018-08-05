using System;

namespace Dawnx
{
    public static partial class DawnArray
    {
        /// <summary>
        /// Do a task for itself.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static TSelf[] Self<TSelf>(this TSelf[] @this, Action<TSelf[], int> task)
        {
            int i = 0;
            foreach (var item in @this)
                task(@this, i++);
            return @this;
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] @this, T element)
        {
            int i = 0;
            foreach (var e in @this)
            {
                if (e.Equals(element))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Retrieves an array from this instance. The new array starts at a specified
        ///     element position and continues to the end of the array.
        ///     (If the parameter is negative, the search will start on the right.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static T[] Slice<T>(this T[] @this, int start) => Slice(@this, start, @this.Length);

        /// <summary>
        /// Retrieves an array from this instance. The new array starts at a specified
        ///     element position and ends with a specified element position.
        ///     (If the parameters is negative, the search will start on the right.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static T[] Slice<T>(this T[] @this, int start, int end)
        {
            start = GetElementPosition(ref @this, start);
            end = GetElementPosition(ref @this, end);

            var length = end - start;
            if (length > 0)
            {
                var ret = new T[length];
                Array.Copy(@this, start, ret, 0, length);
                return ret;
            }
            else if (length == 0) return new T[0];
            else throw new IndexOutOfRangeException($"'start:{start}' can not greater than 'end:{end}'.");
        }
        private static int GetElementPosition<T>(ref T[] str, int pos) => pos < 0 ? str.Length + pos : pos;

        /// <summary>
        /// Shuffles a array and returns itself.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T[] Shuffle<T>(this T[] @this)
        {
            var length = @this.Length;
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                var rnd = random.Next(length);
                var take = @this[i];
                @this[i] = @this[rnd];
                @this[rnd] = take;
            }
            return @this;
        }

        /// <summary>
        /// Projects page elements of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="pageNumber">'pageNumber' starts at 1</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedEnumerable<TSource> SelectPage<TSource>(this TSource[] @this, int pageNumber, int pageSize)
            => new PagedEnumerable<TSource>(@this, pageNumber, pageSize, (int)Math.Ceiling((double)@this.Length / pageSize));

    }
}
