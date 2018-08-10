using System.Collections.Generic;

namespace Dawnx
{
    public static class Range
    {
        /// <summary>
        /// The range type represents an immutable sequence of numbers
        ///     and is commonly used for looping a specific number of times in for loops.
        /// </summary>
        /// <param name="stop"></param>
        public static int[] Create(int stop)
        {
            var range = new List<int>();
            for (int i = 0; i < stop; i++)
                range.Add(i);
            return range.ToArray();
        }

        /// <summary>
        /// The range type represents an immutable sequence of numbers
        ///     and is commonly used for looping a specific number of times in for loops.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        public static int[] Create(int start, int stop)
        {
            var range = new List<int>();
            for (int i = start; i < stop; i++)
                range.Add(i);
            return range.ToArray();
        }

        /// <summary>
        /// The range type represents an immutable sequence of numbers
        ///     and is commonly used for looping a specific number of times in for loops.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="scan"></param>
        public static int[] Create(int start, int stop, int scan)
        {
            var range = new List<int>();
            for (int i = start; i <= stop; i += scan)
                range.Add(i);
            return range.ToArray();
        }

    }
}
