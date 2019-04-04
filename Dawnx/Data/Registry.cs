using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dawnx.Data
{
    /// <summary>
    /// Use Registry[TSelf]
    /// </summary>
    public abstract class Registry
    {
        public string Item { get; private set; }

        public IEnumerable<RegistryStore> ColumnStores { get; private set; }

        public bool ProxyLoaded { get; private set; }

        public void Load<TRegistryStore>(IEnumerable<TRegistryStore> columnStores, string item)
            where TRegistryStore : RegistryStore
        {
            if (GetType().Namespace != "Castle.Proxies")
                throw new InvalidOperationException("This method can only be called in a proxy instance.");

            Item = item;
            ColumnStores = columnStores.Where(x => x.Item == item);
            ProxyLoaded = true;
        }
    }

    /// <summary>
    /// Hint: Each custom properties must be virtual(public).
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    public abstract class Registry<TSelf> : Registry
        where TSelf : Registry<TSelf>, new()
    {
        public static TSelf Connect<TRegistryStore>(IEnumerable<TRegistryStore> columnStores, string item)
            where TRegistryStore : RegistryStore
        {
            var proxy = new TSelf().Proxy(new RegistryProxy<TSelf>());
            proxy.Load(columnStores, item);
            return proxy;
        }
    }

}
