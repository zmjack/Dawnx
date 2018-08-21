using Xunit;

namespace Dawnx.Algorithms.Math.Test
{
    public class MatrixTest
    {
        [Fact]
        public void FindEquationSolution1()
        {
            new Matrix(new double[,]
            {
                { 2, 3, 13 },
                { 7, 8, 38 },
            })
            .Self(_ => Assert.Equal(new double[] { 2, 3 }, _.FindEquationSolution()));
        }

        [Fact]
        public void FindEquationSolution2()
        {
            new Matrix(new double[,]
            {
                { 1, 2, 1, 7 },
                { 2, -1, 3, 7 },
                { 3, 1, 2, 18 },
            })
            .Self(_ => Assert.Equal(new double[] { 7, 1, -2 }, _.FindEquationSolution()));
        }

        [Fact]
        public void FindEquationSolution3()
        {
            new Matrix(new double[,]
            {
                { 27, 9, 3, 1, 0 },
                { 27, 6, 1, 0, 0 },
                { 1, 1, 1, 1, 8 },
                { 3, 2, 1, 0, -1 },
            })
            .Self(_ => Assert.Equal(new double[] { 1.75, -10.25, 14.25, 2.25 }, _.FindEquationSolution()));
        }

        [Fact]
        public void TimesTest()
        {
            var matrix1 = new Matrix(new double[,]
            {
                { 3, 5 },
                { 4, 6 },
            });

            var matrix2 = new Matrix(new double[,]
            {
                { 2 },
                { 3 },
            });

            Assert.Equal(new Matrix(new double[,]
            {
                { 3 * 2 + 5 * 3 },
                { 4 * 2 + 6 * 3 },
            }), matrix1 * matrix2);
        }

        [Fact]
        public void PlusTest()
        {
            var matrix1 = new Matrix(new double[,]
            {
                { 3, 5 },
                { 4, 6 },
            });

            var matrix2 = new Matrix(new double[,]
            {
                { 2, 3 },
                { 3, 4 },
            });

            Assert.Equal(new Matrix(new double[,]
            {
                { 3 + 2, 5 + 3 },
                { 4 + 3, 6 + 4 },
            }), matrix1 + matrix2);
        }

        [Fact]
        public void PowTest()
        {
            var matrix = new Matrix(new double[,]
            {
                { 1, 1 },
                { 1, 0 },
            });

            Assert.Equal(new Matrix(new double[,]
            {
                { 5, 3 },
                { 3, 2 },
            }), matrix.Pow(4));
        }

    }
}
