using Dawnx.Entity;

namespace Dawnx.AspNetCore
{
    public static class DawnIEntityMonitorLog
    {
        public static TEntityMonitorLog FillMonitorLog<TEntityMonitorLog, TEntity>(this TEntityMonitorLog @this, EntityMonitorInvokerParameter<TEntity> param)
            where TEntityMonitorLog : IEntityMonitorLog
            where TEntity : IEntityMonitor
        {
            EntityMonitor.WriteLog(@this, param);
            return @this;
        }

    }
}
