using Dawnx.Utilities;
using System;
using Xunit;

namespace Dawnx.Test
{
    public class DawnDateTimeTest
    {
        [Fact]
        public void Test1()
        {
            /* 2012 - 04
             * 
             *  Su  Mo  Tu  We  Th  Fr  Sa
             *                           1
             *   2   3   4   5   6   7   8
             *   9  10  11  12  13  14  15
             * (16) 17  18  19  20  21  22
             *  23  24  25  26  27  28  29
             *  30
             */

            var today = new DateTime(2012, 4, 16, 22, 23, 24);
            Assert.Equal("2012/4/1 22:23:24", today.FirstDayOfMonth().ToString());
            Assert.Equal("2012/4/30 22:23:24", today.LastDayOfMonth().ToString());

            Assert.Equal("2012/4/9 22:23:24", today.PastDay(DayOfWeek.Monday, false).ToString());
            Assert.Equal("2012/4/16 22:23:24", today.PastDay(DayOfWeek.Monday, true).ToString());
            Assert.Equal("2012/4/23 22:23:24", today.FutureDay(DayOfWeek.Monday, false).ToString());
            Assert.Equal("2012/4/16 22:23:24", today.FutureDay(DayOfWeek.Monday, true).ToString());

            Assert.Equal("2012/4/15 22:23:24", today.PastDay(DayOfWeek.Sunday, false).ToString());
            Assert.Equal("2012/4/15 22:23:24", today.PastDay(DayOfWeek.Sunday, true).ToString());
            Assert.Equal("2012/4/22 22:23:24", today.FutureDay(DayOfWeek.Sunday, false).ToString());
            Assert.Equal("2012/4/22 22:23:24", today.FutureDay(DayOfWeek.Sunday, true).ToString());
            
            Assert.Equal(2, today.WeekInMonth(DayOfWeek.Friday));
            Assert.Equal(3, today.WeekInMonth(DayOfWeek.Sunday));
        }

        [Fact]
        public void TestUnixTimestamp()
        {
            var dt = new DateTime(1970, 1, 1, 16, 0, 0, DateTimeKind.Utc);

            Assert.Equal(57600, dt.UnixTimeSeconds());
            Assert.Equal(57600000, dt.UnixTimeMilliseconds());

            Assert.Equal(dt, DateTimeUtility.FromUnixSeconds(57600));
            Assert.Equal(dt, DateTimeUtility.FromUnixMilliseconds(57600_000));

            Assert.Equal(new DateTime(2018, 10, 31, 15, 55, 17),
                DateTimeUtility.FromUnixSeconds(1540972517).ToLocalTime());
        }

    }
}
