using Xunit;

namespace Dawnx.Algorithms.Math.Test
{
    public class DeterminantTest
    {
        [Fact]
        public void Test1()
        {
            new Determinant(new double[,]
            {
                { 2, 3 },
                { 7, 8 },
            })
            .Self(_ => Assert.Equal(-5, _.Value));
        }

        [Fact]
        public void Test2()
        {
            new Determinant(new double[,]
            {
                { 2, 3, 4 },
                { 3, 4, 5 },
                { 6, 7, 8 },
            })
            .Self(_ => Assert.Equal(0, _.Value));
        }
    }
}
