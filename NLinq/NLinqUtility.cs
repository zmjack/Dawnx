using Dawnx;
using Dawnx.Definition;
using Dawnx.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NLinq.ProviderFunctions;
using System;
using System.Linq;
using System.Reflection;

namespace NLinq
{
    public static partial class NLinqUtility
    {
        public static ValueConverter<TModel, TProvider> BuildConverter<TModel, TProvider>(IProvider<TModel, TProvider> field)
        {
            return new ValueConverter<TModel, TProvider>(v => field.ConvertToProvider(v), v => field.ConvertFromProvider(v));
        }

        public static void ApplyProviderFunctions(DbContext context, ModelBuilder modelBuilder)
        {
            //TODO: To support more providers.
            var providerName = context.GetProviderName();

            switch (providerName)
            {
                case DatabaseProviderName.MySql:
                    modelBuilder.HasDbFunction(typeof(PMySql).GetMethod(nameof(PMySql.Rand)));
                    break;

                default: throw new NotSupportedException();
            }
        }

        public static void ApplyUdFunctions(DbContext context, ModelBuilder modelBuilder)
        {
            var providerName = context.GetProviderName();

            var types = Assembly.GetEntryAssembly().GetTypesWhichImplements<IUdFunctionContainer>();
            var methods = types.SelectMany(type => type.GetMethods().Where(x => x.GetCustomAttribute<UdFunctionAttribute>()?.ProviderName == providerName));
            foreach (var method in methods)
            {
                var attr = method.GetCustomAttribute<UdFunctionAttribute>();
                modelBuilder.HasDbFunction(method, x =>
                {
                    x.HasName(attr.Name);
                    x.HasSchema(attr.Schema);
                });
            }
        }

        public static void ApplyAnnotations(DbContext context, ModelBuilder modelBuilder, NLinqAnnotation annotation = NLinqAnnotation.All)
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

                if ((annotation & NLinqAnnotation.Index) == NLinqAnnotation.Index)
                    ApplyIndexes(entityTypeBuilder, modelClass);
                if ((annotation & NLinqAnnotation.Provider) == NLinqAnnotation.Provider)
                    ApplyProviders(entityTypeBuilder, modelClass);
                if ((annotation & NLinqAnnotation.CompositeKey) == NLinqAnnotation.CompositeKey)
                    ApplyCompositeKey(entityTypeBuilder, modelClass);
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
                    param.PropertyEntries = entry.Properties;

                    EntityMonitor.GetMonitor(entity.GetType().FullName)?.DynamicInvoke(param);
                }
            }
        }


    }
}
