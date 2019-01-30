using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Algorithms.MathAlgorithm
{
    public static class NumberAlgorithm
    {
        /// <summary>
        /// Gets GCD(Greeting Common Divisor) number.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Gcd(int a, int b)
        {
            if (a < 0) throw new ArgumentException($"The argument {nameof(a)} must be non-nagative.");
            if (b < 0) throw new ArgumentException($"The argument {nameof(b)} must be non-nagative.");

            if (a == b)
                return a;
            else if (a > b)
                return b == 0 ? a : Gcd(b, a % b);
            else return Gcd(b, a);
        }
    }
}
