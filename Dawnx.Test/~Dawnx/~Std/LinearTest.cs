using System;
using System.Linq;
using Xunit;

namespace Dawnx.Test
{
    public class LinearTest
    {
        [Fact]
        public void Test1()
        {
            var starts = new[] { new DateTime(2018, 6, 15), new DateTime(2018, 12, 31), new DateTime(2019, 1, 1) };
            var ends = new[] { new DateTime(2018, 7, 15), new DateTime(2019, 1, 1) };

            var linear = Linear.Create(starts, ends);
            Assert.Equal(31, linear.Sum(x => (x.Item2 - x.Item1).TotalDays));
        }
    }
}
