using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.AspNetCore
{
    public static class EntityMonitor
    {
        public static Dictionary<string, object> AddMonitors { get; private set; }
        public static Dictionary<string, object> ModifyMonitors { get; private set; }
        public static Dictionary<string, object> DeleteMonitors { get; private set; }

        public static void RegisterMonitors<TEntity>(EntityState type, Action<IEnumerable<PropertyEntry>> action)
        {
            switch (type)
            {
                case EntityState.Added:
                    AddMonitors[typeof(TEntity).FullName] = action;
                    break;

                case EntityState.Modified:
                    ModifyMonitors[typeof(TEntity).FullName] = action;
                    break;

                case EntityState.Deleted:
                    DeleteMonitors[typeof(TEntity).FullName] = action;
                    break;
            }
        }

        public static Action<IEnumerable<PropertyEntry>> GetMonitor<TEntity>(EntityState type)
            => GetMonitor(typeof(TEntity).FullName, type);
        public static Action<IEnumerable<PropertyEntry>> GetMonitor(string entityFullName, EntityState type)
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

            return action as Action<IEnumerable<PropertyEntry>>;
        }

    }

}
