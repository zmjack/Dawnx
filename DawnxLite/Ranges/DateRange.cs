using Dawnx.Sequences;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Dawnx.Ranges
{
    public class DateRange : IRange<DateTime>
    {
        public enum Unit { Year, Month, Day }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public Unit StepUnit { get; private set; }

        public DateRange(int startYear, int endYear, Unit stepUnit = Unit.Year)
            : this(new DateTime(startYear, 1, 1), new DateTime(endYear, 1, 1), stepUnit) { }
        public DateRange((int Year, int Month) start, (int Year, int Month) end, Unit stepUnit = Unit.Month)
            : this(new DateTime(start.Year, start.Month, 1), new DateTime(end.Year, end.Month, 1), stepUnit) { }
        public DateRange((int Year, int Month, int Day) start, (int Year, int Month, int Day) end, Unit stepUnit = Unit.Day)
            : this(new DateTime(start.Year, start.Month, start.Day), new DateTime(end.Year, end.Month, end.Day), stepUnit) { }
        public DateRange(DateTime start, DateTime end, Unit stepUnit)
        {
            switch (stepUnit)
            {
                case Unit.Day:
                    Start = new DateTime(start.Year, start.Month, start.Day);
                    End = new DateTime(end.Year, end.Month, end.Day);
                    break;

                case Unit.Month:
                    Start = new DateTime(start.Year, start.Month, 1);
                    End = new DateTime(end.Year, end.Month, 1).LastDayOfMonth();
                    break;

                case Unit.Year:
                    Start = new DateTime(start.Year, 1, 1);
                    End = new DateTime(end.Year, 12, 31);
                    break;
            }

            StepUnit = stepUnit;
        }

        public DateTime GetValue(int index)
        {
            switch (StepUnit)
            {
                case Unit.Day: return Start.AddDays(index);
                case Unit.Month: return Start.AddMonths(index);
                case Unit.Year: return Start.AddYears(index);
                default: throw new NotSupportedException();
            }
        }

        public IEnumerator<DateTime> GetEnumerator()
        {
            for (int i = 0; ; i++)
            {
                var value = GetValue(i);
                if (value <= End)
                    yield return value;
                else break;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool IsInRange(DateTime value)
        {
            return Start <= value && value <= End;
        }
    }
}
