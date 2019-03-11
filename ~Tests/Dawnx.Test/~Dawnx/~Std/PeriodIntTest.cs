using Xunit;

namespace Dawnx.Test
{
    public class PeriodIntTest
    {
        [Fact]
        public void Test1()
        {
            var clockPointer = new PeriodIntCalculator(1, 12);
            Assert.Equal(7, clockPointer.From(13) + 6);
        }

    }
}
