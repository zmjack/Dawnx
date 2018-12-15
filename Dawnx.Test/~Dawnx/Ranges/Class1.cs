using Dawnx.Ranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Dawnx.Test.Ranges
{
    public class DateRangeTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(new[]
            {
                new DateTime(2012, 1, 1),
                new DateTime(2013, 1, 1),
                new DateTime(2014, 1, 1),
                new DateTime(2015, 1, 1),
                new DateTime(2016, 1, 1),
            }, new DateRange(new DateTime(2012, 4, 16), new DateTime(2016, 4, 16), DateRange.Unit.Year));

            Assert.Equal(new[]
            {
                new DateTime(2012, 4, 1),
                new DateTime(2012, 5, 1),
                new DateTime(2012, 6, 1),
                new DateTime(2012, 7, 1),
            }, new DateRange(new DateTime(2012, 4, 16), new DateTime(2012, 7, 16), DateRange.Unit.Month));

            Assert.Equal(new[]
            {
                new DateTime(2012, 4, 16),
                new DateTime(2012, 4, 17),
                new DateTime(2012, 4, 18),
            }, new DateRange(new DateTime(2012, 4, 16), new DateTime(2012, 4, 18), DateRange.Unit.Day));
        }

    }
}
