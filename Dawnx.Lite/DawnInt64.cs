
namespace Dawnx
{
    public static class DawnInt64
    {
        /// <summary>
        /// Returns whether the specified number is odd.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsOdd(this long @this) => (@this & 1) == 1;

        /// <summary>
        /// Returns whether the specified number is even.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsEven(this long @this) => (@this & 1) == 0;
    }
}
