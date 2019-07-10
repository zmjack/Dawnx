using System.Collections.Generic;

namespace NLinq
{
    public interface IEntityMonitor
    {
        object MonitorCarry { get; set; }
    }

    public static class DawnIEntityMonitor
    {
        public static TEntityMonitor MonitorCarry<TEntityMonitor>(this TEntityMonitor @this, object carry)
            where TEntityMonitor : IEntityMonitor
        {
            @this.MonitorCarry = carry;
            return @this;
        }
        public static IEnumerable<TEntityMonitor> MonitorCarry<TEntityMonitor>(this IEnumerable<TEntityMonitor> @this, object carry)
            where TEntityMonitor : IEntityMonitor
        {
            foreach (var monitor in @this)
                MonitorCarry(monitor, carry);

            return @this;
        }

    }

}
