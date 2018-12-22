using Dawnx.Utilities;
using System;
using System.Linq;
using Xunit;

namespace Dawnx.Test.Utilities
{
    public class DateTimeUtilityTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(49, DateTimeUtility.GetMonths(new DateTime(2012, 4, 16), new DateTime(2016, 4, 18)).Count());

            Assert.Equal(3, DateTimeUtility.GetDays(new DateTime(2012, 4, 16), new DateTime(2012, 4, 18)).Count());
            Assert.Equal(30, DateTimeUtility.GetDays(new DateTime(2012, 4, 16), new DateTime(2012, 5, 15)).Count());
        }

    }
}
