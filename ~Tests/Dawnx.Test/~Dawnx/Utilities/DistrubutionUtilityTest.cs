using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dawnx.Utilities.Test
{
    public class DistributionUtilityTest
    {
        [Fact]
        public void DistributeTest()
        {
            var amount = 20;
            var method = DistributionUtility.DistributionMethod.FormerPreferred;
            var result = DistributionUtility.Distribute(amount, method, out var rest, new[] { 4, 2, 8, 10 });
            Assert.Equal(new[] { 4, 2, 8, 6 }, result);
            Assert.Equal(0, rest);
        }

    }
}
