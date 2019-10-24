using Dawnx;
using Dawnx.Reflection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NLinq
{
    public static partial class NLinqUtility
    {
        private static void ApplyCompositeKey(object entityTypeBuilder, Type modelClass)
        {
            var hasKeyMethod = entityTypeBuilder.GetType().GetMethod(nameof(EntityTypeBuilder.HasKey), new[] { typeof(string[]) });

            var modelProps = modelClass.GetProperties()
                .Where(x => x.GetCustomAttribute<CPKeyAttribute>() != null)
                .OrderBy(x => x.GetCustomAttribute<CPKeyAttribute>().Order);
            var propNames = modelProps.Select(x => x.Name).ToArray();

            if (propNames.Any())
                hasKeyMethod.Invoke(entityTypeBuilder, new object[] { propNames });
        }

        private static void ApplyIndexes(object entityTypeBuilder, Type modelClass)
        {
            var hasIndexMethod = entityTypeBuilder.GetType().GetMethod(nameof(EntityTypeBuilder.HasIndex), new[] { typeof(string[]) });
            var propPairs = modelClass.GetProperties().Select(prop => new
            {
                Attribute = prop.GetCustomAttribute<IndexAttribute>(),
                Property = prop,
            }).Where(x => x.Attribute != null);

            foreach (var pair in propPairs.Where(x => x.Attribute.Group == null))
            {
                var indexBuilder = hasIndexMethod.Invoke(entityTypeBuilder, new object[] { new[] { pair.Property.Name } }) as IndexBuilder;
                if (pair.Attribute.Type == IndexType.Unique)
                    indexBuilder.IsUnique();
            }
            foreach (var pairGroup in propPairs.Where(x => x.Attribute.Group != null).GroupBy(x => x.Attribute.Group))
            {
                var normalPairs = pairGroup.Where(x => x.Attribute.Type == IndexType.Normal);
                if (normalPairs.Any())
                    hasIndexMethod.Invoke(entityTypeBuilder, new object[] { normalPairs.Select(x => x.Property.Name).ToArray() });

                var uniquePairs = pairGroup.Where(x => x.Attribute.Type == IndexType.Unique);
                if (uniquePairs.Any())
                {
                    var indexBuilder = hasIndexMethod.Invoke(entityTypeBuilder, new object[] { uniquePairs.Select(x => x.Property.Name).ToArray() }) as IndexBuilder;
                    indexBuilder.IsUnique();
                }
            }
        }

        private static void ApplyProviders(object entityTypeBuilder, Type modelClass)
        {
            var propertyMethod = entityTypeBuilder.GetType().GetMethodViaQualifiedName("Microsoft.EntityFrameworkCore.Metadata.Builders.PropertyBuilder Property(System.String)");

            var modelProps = modelClass.GetProperties();
            foreach (var modelProp in modelProps)
            {
                var attr = modelProp.GetCustomAttribute<ProviderAttribute>();
                if (attr != null)
                {
                    var propertyBuilder = propertyMethod.Invoke(entityTypeBuilder, new object[] { modelProp.Name }) as PropertyBuilder;
                    var hasConversionMethod = typeof(PropertyBuilder).GetMethod(nameof(PropertyBuilder.HasConversion), new[] { typeof(ValueConverter) });

                    dynamic provider = Activator.CreateInstance(attr.ProviderType);
                    hasConversionMethod.Invoke(propertyBuilder, new object[] { NLinqUtility.BuildConverter(provider) });
                }
            }
        }

        private static void ResolveTrackAttributes(EntityEntry entry, PropertyInfo[] properties)
        {
            var props_TrackCreationTime = properties.Where(x => x.HasCustomAttribute<TrackCreationTimeAttribute>());
            var props_TrackLastWrite = properties.Where(x => x.HasCustomAttribute<TrackLastWriteTimeAttribute>());
            var props_TrackLower = properties.Where(x => x.HasCustomAttribute<TrackLowerAttribute>());
            var props_TrackUpper = properties.Where(x => x.HasCustomAttribute<TrackUpperAttribute>());
            var props_TrackTrim = properties.Where(x => x.HasCustomAttribute<TrackTrimAttribute>());
            var props_TrackCondensed = properties.Where(x => x.HasCustomAttribute<TrackCondensedAttribute>());
            var props_Track = properties.Where(x => x.HasCustomAttribute<TrackAttribute>());

            var now = DateTime.Now;
            switch (entry.State)
            {
                case EntityState.Added:
                    SetPropertiesValue(props_TrackCreationTime, entry, v => now);
                    SetPropertiesValue(props_TrackLastWrite, entry, v => now);
                    SetPropertiesValue(props_TrackLower, entry, v => (v as string)?.ToLower());
                    SetPropertiesValue(props_TrackUpper, entry, v => (v as string)?.ToUpper());
                    SetPropertiesValue(props_TrackTrim, entry, v => (v as string)?.Trim());
                    SetPropertiesValue(props_TrackCondensed, entry, v => ((v as string) ?? "").Unique());
                    SetPropertiesValueForTrack(props_Track, entry);
                    break;

                case EntityState.Modified:
                    SetPropertiesValue(props_TrackLastWrite, entry, v => now);
                    SetPropertiesValue(props_TrackLower, entry, v => (v as string)?.ToLower());
                    SetPropertiesValue(props_TrackUpper, entry, v => (v as string)?.ToUpper());
                    SetPropertiesValue(props_TrackTrim, entry, v => (v as string)?.Trim());
                    SetPropertiesValue(props_TrackCondensed, entry, v => (v as string)?.Unique());
                    SetPropertiesValueForTrack(props_Track, entry);
                    break;
            }
        }

        private static void SetPropertiesValue(IEnumerable<PropertyInfo> properties, EntityEntry entry,
            Func<object, object> evalMethod)
        {
            foreach (var prop in properties)
            {
                var oldValue = prop.GetValue(entry.Entity);
                prop.SetValue(entry.Entity, evalMethod(oldValue));
            }
        }

        private static void SetPropertiesValueForTrack(IEnumerable<PropertyInfo> properties, EntityEntry entry)
        {
            foreach (var prop in properties)
            {
                var trackAttr = prop.GetCustomAttribute<TrackAttribute>();

                var type = trackAttr.Type;
                var csharp = trackAttr.CSharp;
                var entity = entry.Entity;
                var entityType = entity.GetType()
                    .For(self => self.Module.FullyQualifiedName != "<In Memory Module>" ? self : self.BaseType);

                Script shell;
                if (type != null)
                {
                    var references = new[] { type.Assembly.FullName };

                    //If the invoked method is 'Method<T>(this T @this),
                    //  the correct pattern is '@this.Method'
                    shell = CSharpScript.Create($"using static {type.Namespace}.{type.Name};",
                        ScriptOptions.Default.AddReferences(references), entityType)
                        .ContinueWith(csharp);
                }
                else shell = CSharpScript.Create(csharp, ScriptOptions.Default, entityType);

                var scriptState = shell.RunAsync(entity).Result;
                prop.SetValue(entity, scriptState.ReturnValue);
            }
        }

    }
}
