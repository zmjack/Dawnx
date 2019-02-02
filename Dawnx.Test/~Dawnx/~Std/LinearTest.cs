using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            
            var linear = Linear.Create(starts, ends, (start, end) => new { Start = start, End = end });
            Assert.Equal(31, linear.Sum(x => (x.End - x.Start).TotalDays));
        }
    }
}
