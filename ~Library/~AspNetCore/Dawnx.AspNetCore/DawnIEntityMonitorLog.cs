using Dawnx.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.AspNetCore
{
    public static class DawnIEntityMonitorLog
    {
        public static TEntityMonitorLog Fill<TEntityMonitorLog, TEntity>(this TEntityMonitorLog @this, EntityMonitorInvokerParameter<TEntity> param)
            where TEntityMonitorLog : IEntityMonitorLog
            where TEntity : IEntityMonitor
        {
            EntityMonitor.WriteLog(@this, param);
            return @this;
        }

    }
}
