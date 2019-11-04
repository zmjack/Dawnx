using NStandard;
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
            Assert.Equal(49, DateTimeEx.GetMonths(new DateTime(2012, 4, 16), new DateTime(2016, 4, 18)).Count());

            Assert.Equal(3, DateTimeEx.GetDays(new DateTime(2012, 4, 16), new DateTime(2012, 4, 18)).Count());
            Assert.Equal(30, DateTimeEx.GetDays(new DateTime(2012, 4, 16), new DateTime(2012, 5, 15)).Count());

        }

        [Fact]
        public void DateDifTest()
        {
            Assert.Equal(3, DateTimeEx.GetCompleteYears(new DateTime(1980, 7, 28), new DateTime(1984, 3, 27)));
        }

        [Fact]
        public void WeekTest()
        {
            /* 2019 - 01
             * 
             *  Su  Mo  Tu  We  Th  Fr  Sa
             *           1   2   3   4   5
             *   6   7   8   9  10  11  12
             *  13  14  15  16  17  18  19
             * (20) 21  22  23  24  25  26
             *  27  28  29  30  31
             */
            Assert.Equal(new DateTime(2019, 1, 7), DateTimeEx.ParseFromWeek(2019, 1, DayOfWeek.Monday));
            Assert.Equal(new DateTime(2019, 1, 1), DateTimeEx.ParseFromWeek(2019, 1, DayOfWeek.Tuesday));
            Assert.Equal(new DateTime(2019, 1, 2), DateTimeEx.ParseFromWeek(2019, 1, DayOfWeek.Wednesday));
            Assert.Equal(new DateTime(2019, 1, 3), DateTimeEx.ParseFromWeek(2019, 1, DayOfWeek.Thursday));
            Assert.Equal(new DateTime(2019, 1, 4), DateTimeEx.ParseFromWeek(2019, 1, DayOfWeek.Friday));
            Assert.Equal(new DateTime(2019, 1, 5), DateTimeEx.ParseFromWeek(2019, 1, DayOfWeek.Saturday));
            Assert.Equal(new DateTime(2019, 1, 6), DateTimeEx.ParseFromWeek(2019, 1, DayOfWeek.Sunday));
        }


    }
}
