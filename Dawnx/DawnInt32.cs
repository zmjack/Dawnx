
namespace Dawnx
{
    public static class DawnInt32
    {
        /// <summary>
        /// Returns whether the specified number is odd.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsOdd(this int @this) => (@this & 1) == 1;

        /// <summary>
        /// Returns whether the specified number is even.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsEven(this int @this) => (@this & 1) == 0;
    }
}
