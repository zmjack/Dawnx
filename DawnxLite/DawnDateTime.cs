using Dawnx.Utilities;
using System;
using System.Collections;
using System.Linq;

namespace Dawnx
{
    public static class DawnDateTime
    {
        /// <summary>
        /// Gets the first day of the month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime @this)
            => @this.AddDays(1 - @this.Day);

        /// <summary>
        /// Gets the last day of the month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(this DateTime @this)
            => @this.AddDays(DateTime.DaysInMonth(@this.Year, @this.Month) - @this.Day);

        /// <summary>
        /// Gets a past day for the specified day of week.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="includeCurrentDay"></param>
        /// <returns></returns>
        public static DateTime PastDay(this DateTime @this, DayOfWeek dayOfWeek, bool includeCurrentDay = false)
        {
            var days = dayOfWeek - @this.DayOfWeek;

            if (!includeCurrentDay && days == 0)
                return @this.AddDays(-7);

            return @this.AddDays(CastCycleDays(days, true));
        }

        /// <summary>
        /// Gets a future day for the specified day of week.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="includeCurrentDay"></param>
        /// <returns></returns>
        public static DateTime FutureDay(this DateTime @this, DayOfWeek dayOfWeek, bool includeCurrentDay = false)
        {
            var days = dayOfWeek - @this.DayOfWeek;

            if (!includeCurrentDay && days == 0)
                return @this.AddDays(7);

            return @this.AddDays(CastCycleDays(days, false));
        }

        /// <summary>
        /// Gets the number of weeks in a month for the specified date.
        /// (eg. If define Sunday as the fisrt day of the week, its first appearance means week 1, before is week 0.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="weekStart"></param>
        /// <returns></returns>
        public static int WeekInMonth(this DateTime @this, DayOfWeek weekStart)
        {
            var day1 = new DateTime(@this.Year, @this.Month, 1);
            var day1Week_day1 = PastDay(day1, weekStart, false);

            if (day1Week_day1.Month == @this.Month)
                return (PastDay(@this, weekStart, false) - day1Week_day1).Days / 7 + 1;
            else return (PastDay(@this, weekStart, false) - day1Week_day1).Days / 7;
        }

        /// <summary>
        /// Gets the number of weeks in a year for the specified date. 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="weekStart"></param>
        /// <returns></returns>
        public static int Week(this DateTime @this, DayOfWeek weekStart)
        {
            var day1 = new DateTime(@this.Year, 1, 1);
            var day1Week_day1 = PastDay(day1, weekStart, false);

            if (day1Week_day1.Year == @this.Year)
                return (PastDay(@this, weekStart, false) - day1Week_day1).Days / 7 + 1;
            else return (PastDay(@this, weekStart, false) - day1Week_day1).Days / 7;
        }

        /// <summary>
        /// Gets the Unix Timestamp(milliseconds) of the specified DateTime(UTC).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long UnixTimeMilliseconds(this DateTime @this)
            => DateTimeUtility.ToUnixTimeMilliseconds(@this);

        /// <summary>
        /// Gets the Unix Timestamp(seconds) of the specified DateTime(UTC).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long UnixTimeSeconds(this DateTime @this)
            => DateTimeUtility.ToUnixTimeSeconds(@this);

        /// <summary>
        /// Get the start point of the sepecified month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime BeginningOfYear(this DateTime @this) => new DateTime(@this.Year, 1, 1, 0, 0, 0, 0, @this.Kind);

        /// <summary>
        /// Get the end point of the sepecified month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime EndOfYear(this DateTime @this) => new DateTime(@this.Year, 12, 31, 23, 59, 59, 999, @this.Kind);

        /// <summary>
        /// Get the start point of the sepecified month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime BeginningOfMonth(this DateTime @this) => new DateTime(@this.Year, @this.Month, 1, 0, 0, 0, 0, @this.Kind);

        /// <summary>
        /// Get the end point of the sepecified month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime EndOfMonth(this DateTime @this)
        {
            if (new[] { 1, 3, 5, 7, 8, 10, 12 }.Contains(@this.Month))
                return new DateTime(@this.Year, @this.Month, 31, 23, 59, 59, 999, @this.Kind);
            else if (new[] { 4, 6, 9, 11 }.Contains(@this.Month))
                return new DateTime(@this.Year, @this.Month, 30, 23, 59, 59, 999, @this.Kind);
            else
            {
                if (DateTime.IsLeapYear(@this.Year))
                    return new DateTime(@this.Year, @this.Month, 29, 23, 59, 59, 999, @this.Kind);
                else return new DateTime(@this.Year, @this.Month, 28, 23, 59, 59, 999, @this.Kind);
            }
        }

        /// <summary>
        /// Get the start point of the sepecified day.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime BeginningOfDay(this DateTime @this) => @this.Date;

        /// <summary>
        /// Get the end point of the sepecified day.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime @this) => new DateTime(@this.Year, @this.Month, @this.Day, 23, 59, 59, 999, @this.Kind);

        private static int CastCycleDays(int days, bool isBackward)
        {
            days = days % 7;

            if (isBackward)
                return days > 0 ? days - 7 : days;
            else return days < 0 ? days + 7 : days;
        }



    }
}
