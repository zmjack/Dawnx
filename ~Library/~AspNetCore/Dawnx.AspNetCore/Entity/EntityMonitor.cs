using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.AspNetCore.Entity
{
    public delegate void MonitorInvoker(object state, object model, IEnumerable<PropertyEntry> propertyEntries);

    public static class EntityMonitor
    {
        public static Dictionary<string, object> AddMonitors { get; private set; } = new Dictionary<string, object>();
        public static Dictionary<string, object> ModifyMonitors { get; private set; } = new Dictionary<string, object>();
        public static Dictionary<string, object> DeleteMonitors { get; private set; } = new Dictionary<string, object>();

        public static void Register<TEntity>(EntityState type, MonitorInvoker invoker)
        {
            switch (type)
            {
                case EntityState.Added:
                    AddMonitors[typeof(TEntity).FullName] = invoker;
                    break;

                case EntityState.Modified:
                    ModifyMonitors[typeof(TEntity).FullName] = invoker;
                    break;

                case EntityState.Deleted:
                    DeleteMonitors[typeof(TEntity).FullName] = invoker;
                    break;

                default: throw new NotSupportedException("Only Added, Modified, Deleted are supported.");
            }
        }

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
