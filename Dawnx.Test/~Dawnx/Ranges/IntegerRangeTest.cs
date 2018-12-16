using Dawnx.Ranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Dawnx.Test.Ranges
{
    public class IntegerRangeTest
    {
        [Fact]
        public void IsInRangeTest()
        {
            foreach (var item in new[] { 4, 9, 14 })
                Assert.True(new IntegerRange(4, 16, 5).IsInRange(item));
            Assert.False(new IntegerRange(4, 16, 5).IsInRange(16));
        }

    }
}
