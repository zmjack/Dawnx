using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dawnx.AspNetCore
{
    public static class DawnDbContext
    {
        /// <summary>
        /// This method should be called before 'base.SaveChanges'.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public static void IntelliTrack(this DbContext @this, bool acceptAllChangesOnSuccess)
        {
            var entries = @this.ChangeTracker.Entries().ToArray();
            foreach (var entry in entries)
            {
                var allProps = entry.Entity.GetType().GetProperties().Where(x => x.CanWrite).ToArray();
                ResolveTrackAttributes(entry, allProps);
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
                    SetPropertiesValue(props_TrackCondensed, entry, v => (v as string)?.Unique());
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
                var entityType = entity.GetType().For(_ => _.Module.FullyQualifiedName != "<In Memory Module>" ? _ : _.BaseType);

                Script shell;
                if (type != null)
                {
                    var references = new[] { type.Assembly.FullName };

                    //If the invoked method is 'Method<T>(this T @this),
                    //  then the correct pattern is '@this.Method'
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
