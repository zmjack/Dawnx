using Dawnx.Ranges;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx
{
    public partial class Range
    {
        /// <summary>
        /// The range type represents an immutable sequence of numbers
        ///     and is commonly used for looping a specific number of times in for loops.
        /// </summary>
        /// <param name="stop"></param>
        public static int[] Create(int stop) => new IntegerRange(stop - 1).ToArray();

        /// <summary>
        /// The range type represents an immutable sequence of numbers
        ///     and is commonly used for looping a specific number of times in for loops.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        public static int[] Create(int start, int stop) => new IntegerRange(start, stop - 1).ToArray();

        /// <summary>
        /// The range type represents an immutable sequence of numbers
        ///     and is commonly used for looping a specific number of times in for loops.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="scan"></param>
        public static int[] Create(int start, int stop, int scan) => new IntegerRange(start, stop - 1, scan).ToArray();

    }
}
