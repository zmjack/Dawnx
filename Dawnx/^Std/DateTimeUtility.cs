using System;

namespace Dawnx
{
    public static class DateTimeUtility
    {
        public static DateTime UnixMinValue() => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}
