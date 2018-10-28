using Dawnx.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace Dawnx.AspNetCore
{
    public delegate void MonitorInvoker<TEntity>(TEntity model, dynamic carry, IEnumerable<PropertyEntry> propertyEntries)
        where TEntity : IEntityMonitor;

    public static class EntityMonitor
    {
        public static Dictionary<string, Delegate> AddMonitors { get; private set; } = new Dictionary<string, Delegate>();
        public static Dictionary<string, Delegate> ModifyMonitors { get; private set; } = new Dictionary<string, Delegate>();
        public static Dictionary<string, Delegate> DeleteMonitors { get; private set; } = new Dictionary<string, Delegate>();

        public static void RegisterForAdded<TEntity>(MonitorInvoker<TEntity> invoker)
            where TEntity : IEntityMonitor
            => AddMonitors[typeof(TEntity).FullName] = invoker;
        public static void RegisterForModified<TEntity>(MonitorInvoker<TEntity> invoker)
            where TEntity : IEntityMonitor
            => ModifyMonitors[typeof(TEntity).FullName] = invoker;
        public static void RegisterForDeleted<TEntity>(MonitorInvoker<TEntity> invoker)
            where TEntity : IEntityMonitor
            => DeleteMonitors[typeof(TEntity).FullName] = invoker;

        public static MonitorInvoker<TEntity> GetMonitor<TEntity>(EntityState type)
            where TEntity : IEntityMonitor
            => GetMonitor(typeof(TEntity).FullName, type) as MonitorInvoker<TEntity>;
        public static Delegate GetMonitor(string entityFullName, EntityState type)
        {
            Delegate action = null;
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

            return action;
        }

    }

}
