using Dawnx.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;

namespace NLinq
{
    public static partial class NLinqUtility
    {
        public static ValueConverter<TModel, TProvider> BuildConverter<TModel, TProvider>(IProvider<TModel, TProvider> field)
        {
            return new ValueConverter<TModel, TProvider>(v => field.ConvertToProvider(v), v => field.ConvertFromProvider(v));
        }

        public static void Apply(DbContext context, ModelBuilder modelBuilder)
        {
            var entityMethod = modelBuilder.GetType()
                .GetMethodViaQualifiedName("Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder`1[TEntity] Entity[TEntity]()");
            var dbSetProps = context.GetType().GetProperties()
                .Where(x => x.ToString().StartsWith("Microsoft.EntityFrameworkCore.DbSet`1"));

            foreach (var dbSetProp in dbSetProps)
            {
                var modelClass = dbSetProp.PropertyType.GenericTypeArguments[0];
                var entityMethod1 = entityMethod.MakeGenericMethod(modelClass);
                var entityTypeBuilder = entityMethod1.Invoke(modelBuilder, new object[0]);

                ApplyIndexes(entityTypeBuilder, modelClass);
                ApplyProviders(entityTypeBuilder, modelClass);
            }
        }

        /// <summary>
        /// This method should be called before 'base.SaveChanges'.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public static void IntelliTrack(DbContext @this, bool acceptAllChangesOnSuccess)
        {
            var entries = @this.ChangeTracker.Entries()
                .Where(x => new[] { EntityState.Added, EntityState.Modified, EntityState.Deleted }.Contains(x.State))
                .ToArray();

            foreach (var entry in entries)
            {
                // Resolve TrackAttributes
                var entity = entry.Entity;
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    var props = entity.GetType().GetProperties().Where(x => x.CanWrite).ToArray();
                    ResolveTrackAttributes(entry, props);
                }

                // Resolve Monitors
                var entityMonitor = entity as IEntityMonitor;
                if (!(entityMonitor is null))
                {
                    var paramType = typeof(EntityMonitorInvokerParameter<>).MakeGenericType(entity.GetType());
                    var param = Activator.CreateInstance(paramType) as IEntityMonitorInvokerParameter;
                    param.State = entry.State;
                    param.Entity = entity;
                    param.Carry = entityMonitor.MonitorCarry;
                    param.PropertyEntries = entry.Properties;

                    EntityMonitor.GetMonitor(entity.GetType().FullName)?.DynamicInvoke(param);
                }
            }
        }


    }
}
