using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.AspNetCore.Entity
{
    public delegate void MonitorInvoker(object model, dynamic carry, IEnumerable<PropertyEntry> propertyEntries);

    public static class EntityMonitor
    {
        public static Dictionary<string, object> AddMonitors { get; private set; } = new Dictionary<string, object>();
        public static Dictionary<string, object> ModifyMonitors { get; private set; } = new Dictionary<string, object>();
        public static Dictionary<string, object> DeleteMonitors { get; private set; } = new Dictionary<string, object>();

        public static void RegisterForAdded<TEntity>(MonitorInvoker invoker)
            => AddMonitors[typeof(TEntity).FullName] = invoker;
        public static void RegisterForModified<TEntity>(MonitorInvoker invoker)
            => ModifyMonitors[typeof(TEntity).FullName] = invoker;
        public static void RegisterForDeleted<TEntity>(MonitorInvoker invoker)
            => DeleteMonitors[typeof(TEntity).FullName] = invoker;

        public static MonitorInvoker GetMonitor<TEntity>(EntityState type)
            => GetMonitor(typeof(TEntity).FullName, type);
        public static MonitorInvoker GetMonitor(string entityFullName, EntityState type)
        {
            object action = null;
            switch (type)
            {
                case EntityState.Added:
                    AddMonitors.TryGetValue(entityFullName, out action);
                    break;

                case EntityState.Modified:
                    ModifyMonitors.TryGetValue(entityFullName, out action);
                    break;

                case EntityState.Deleted:
                    DeleteMonitors.TryGetValue(entityFullName, out action);
                    break;
            }

            return action as MonitorInvoker;
        }

    }

}
