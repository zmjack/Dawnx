using System.Collections.Generic;

namespace Dawnx.Entity
{
    public interface IEntityMonitor
    {
        object MonitorState { get; set; }
    }

    public static class DawnIEntityMonitor
    {
        public static TEntityMonitor EnableMonitor<TEntityMonitor>(this TEntityMonitor @this, object state)
            where TEntityMonitor : IEntityMonitor
        {
            @this.MonitorState = state;
            return @this;
        }
        public static IEnumerable<TEntityMonitor> EnableMonitor<TEntityMonitor>(this IEnumerable<TEntityMonitor> @this, object state)
            where TEntityMonitor : IEntityMonitor
        {
            @this.Each(_ => EnableMonitor(_, state));
            return @this;
        }

        public static TEntityMonitor DisableMonitor<TEntityMonitor>(this TEntityMonitor @this)
            where TEntityMonitor : IEntityMonitor
        {
            @this.MonitorState = null;
            return @this;
        }
        public static IEnumerable<TEntityMonitor> DisableMonitor<TEntityMonitor>(this IEnumerable<TEntityMonitor> @this)
            where TEntityMonitor : IEntityMonitor
        {
            @this.Each(_ => DisableMonitor(_));
            return @this;
        }

        public static bool IsMonitorEnabled(this IEntityMonitor @this)
            => !(@this.MonitorState is null);

    }

}
