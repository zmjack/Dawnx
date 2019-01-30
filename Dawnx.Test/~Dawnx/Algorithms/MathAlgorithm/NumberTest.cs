using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dawnx.Algorithms.MathAlgorithm.Test
{
    public class NumberAlgorithmTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(24, NumberAlgorithm.Gcd(24, 0));
            Assert.Equal(8, NumberAlgorithm.Gcd(24, 8));
            Assert.Equal(4, NumberAlgorithm.Gcd(24, 20));

            Assert.Equal(24, NumberAlgorithm.Gcd(0, 24));
            Assert.Equal(8, NumberAlgorithm.Gcd(8, 24));
            Assert.Equal(4, NumberAlgorithm.Gcd(20, 24));
        }
    }
}
