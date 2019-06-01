using System.Collections.Generic;

namespace Dawnx.Entity
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
            @this.Each(_ => MonitorCarry(_, carry));
            return @this;
        }

    }

}
