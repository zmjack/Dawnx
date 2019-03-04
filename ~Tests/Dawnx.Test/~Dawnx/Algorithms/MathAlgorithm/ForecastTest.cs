using Dawnx.Ranges;
using System.Linq;
using Xunit;

namespace Dawnx.Algorithms.MathAlgorithm.Test
{
    public class ForecastTest
    {
        [Fact]
        public void Test1()
        {
            var result = Forecast.Linear(
                  known_x: new IntegerRange(2010, 2015).Select(x => (double)x).ToArray(),
                  known_y: new[] { 249_000.00, 300_000.00, 250_000.00, 340_000.00, 540_000.00, 367_000.00 },
                  forecast_x: 2016);
            Assert.Equal(481000, result, 0);
        }
    }
}
