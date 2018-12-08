using System;
using System.Collections.Generic;

namespace Dawnx.Utilities
{
    public static class DateTimeUtility
    {
        /// <summary>
        /// Gets the DateTime(UTC) of UnixMinValue.
        /// </summary>
        /// <returns></returns>
        public static DateTime UnixMinValue() => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts the sepecified Unix TimeStamp(seconds) to DateTime(UTC).
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static DateTime FromUnixSeconds(long seconds)
            => FromUnixMilliseconds(seconds * 1000);

        /// <summary>
        /// Converts the sepecified Unix TimeStamp(milliseconds) to DateTime(UTC).
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DateTime FromUnixMilliseconds(long milliseconds)
            => new DateTime(milliseconds * 10000 + 621355968000000000, DateTimeKind.Utc);

        /// <summary>
        /// Gets the Unix Timestamp(milliseconds) of the specified DateTime(UTC).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(DateTime @this)
            => (@this.ToUniversalTime().Ticks - 621355968000000000) / 10000;

        /// <summary>
        /// Gets the Unix Timestamp(seconds) of the specified DateTime(UTC).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(DateTime @this)
            => ToUnixTimeMilliseconds(@this) / 1000;

        /// <summary>
        /// Gets the range of months.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetMonths(DateTime startDate, DateTime endDate)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, 1);
            endDate = new DateTime(endDate.Year, endDate.Month, 1);

            for (var dt = startDate; dt <= endDate; dt = dt.AddMonths(1))
                yield return dt;
        }

        /// <summary>
        /// Gets the range of days.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetDays(DateTime startDate, DateTime endDate)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day);

            for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
                yield return dt;
        }

    }
}
