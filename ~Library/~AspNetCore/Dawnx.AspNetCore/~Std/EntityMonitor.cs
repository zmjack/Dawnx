using Dawnx.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace Dawnx.AspNetCore
{
    public delegate void CommonMonitorInvoker<TEntity>(EntityState state, TEntity model, dynamic carry, IEnumerable<PropertyEntry> propertyEntries)
        where TEntity : IEntityMonitor;
    public delegate void StateMonitorInvoker<TEntity>(TEntity model, dynamic carry, IEnumerable<PropertyEntry> propertyEntries)
        where TEntity : IEntityMonitor;

    public static class EntityMonitor
    {
        public static Dictionary<string, Delegate> AddedMonitors { get; private set; } = new Dictionary<string, Delegate>();
        public static Dictionary<string, Delegate> ModifiedMonitors { get; private set; } = new Dictionary<string, Delegate>();
        public static Dictionary<string, Delegate> DeletedMonitors { get; private set; } = new Dictionary<string, Delegate>();
        public static Dictionary<string, Delegate> Monitors { get; private set; } = new Dictionary<string, Delegate>();

        public static void RegisterForAdded<TEntity>(StateMonitorInvoker<TEntity> invoker)
            where TEntity : IEntityMonitor
            => AddedMonitors[typeof(TEntity).FullName] = invoker;
        public static void RegisterForModified<TEntity>(StateMonitorInvoker<TEntity> invoker)
            where TEntity : IEntityMonitor
            => ModifiedMonitors[typeof(TEntity).FullName] = invoker;
        public static void RegisterForDeleted<TEntity>(StateMonitorInvoker<TEntity> invoker)
            where TEntity : IEntityMonitor
            => DeletedMonitors[typeof(TEntity).FullName] = invoker;
        public static void Register<TEntity>(CommonMonitorInvoker<TEntity> invoker)
            where TEntity : IEntityMonitor
            => Monitors[typeof(TEntity).FullName] = invoker;

        public static Delegate GetMonitor(string entityFullName, EntityState type)
        {
            Delegate action = null;
            switch (type)
            {
                case EntityState.Added:
                    AddedMonitors.TryGetValue(entityFullName, out action);
                    break;

                case EntityState.Modified:
                    ModifiedMonitors.TryGetValue(entityFullName, out action);
                    break;

                case EntityState.Deleted:
                    DeletedMonitors.TryGetValue(entityFullName, out action);
                    break;
            }

            return action;
        }

        public static Delegate GetCommonMonitor(string entityFullName)
        {
            Monitors.TryGetValue(entityFullName, out var action);
            return action;
        }

    }

}
