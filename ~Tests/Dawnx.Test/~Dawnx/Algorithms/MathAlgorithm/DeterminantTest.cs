using NStandard;
using Xunit;

namespace Dawnx.Algorithms.MathAlgorithm.Test
{
    public class DeterminantTest
    {
        [Fact]
        public void Test1()
        {
            var determinant = new Determinant(new double[,]
            {
                { 2, 3 },
                { 7, 8 },
            });
            Assert.Equal(-5, determinant.Value);
        }

        [Fact]
        public void Test2()
        {
            var determinant = new Determinant(new double[,]
            {
                { 2, 3, 4 },
                { 3, 4, 5 },
                { 6, 7, 8 },
            });
            Assert.Equal(0, determinant.Value);
        }
    }
}
