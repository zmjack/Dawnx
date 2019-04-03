using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dawnx.Data
{
    public interface IColumnStorable<TIColumnStore>
        where TIColumnStore : IColumnStore
    {
        IEnumerable<TIColumnStore> ColumnStores { get; set; }
        bool ProxyLoaded { get; set; }
    }

    public static class IColumnStorableExtension
    {
        public static void Load<TIColumnStore>(this IColumnStorable<TIColumnStore> @this, IEnumerable<TIColumnStore> columnStores)
            where TIColumnStore : IColumnStore
        {
            if (@this.GetType().Namespace != "Castle.Proxies")
                throw new InvalidOperationException("This method can only be called in a proxy instance.");

            if (!columnStores.Any()) return;
            if (columnStores.Select(x => x.EntityId).Distinct().Count() > 1)
                throw new InvalidCastException("The `EntityId` of each column stores must be same.");
            var entityId = columnStores.First().EntityId;
            var props = @this.GetType().GetProperties();

            var keys = props.Where(x => x.GetCustomAttribute<KeyAttribute>() != null);
            if (keys.Count() > 1)
                throw new NotSupportedException("Only one primary key is supported.");
            foreach (var key in keys)
            {
                if (key.PropertyType == typeof(string))
                    key.SetValue(@this, entityId);
                else if (key.PropertyType == typeof(Guid))
                    key.SetValue(@this, Guid.Parse(entityId));
                else throw new NotSupportedException("Only string and Guid are supported.");
            }

            @this.ColumnStores = columnStores;
            @this.ProxyLoaded = true;
        }
    }

}
