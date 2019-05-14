using System;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx.Data
{
    /// <summary>
    /// Use Registry[TSelf]
    /// </summary>
    public abstract class Registry
    {
        private string _Item;
        private IEnumerable<RegistryStore> _ColumnStores;
        private bool _ProxyLoaded;

        public string GetItemString() => _Item;
        public IEnumerable<RegistryStore> GetColumnStores() => _ColumnStores;
        public bool IsProxyLoaded() => _ProxyLoaded;

        public void Load<TRegistryStore>(IEnumerable<TRegistryStore> columnStores, string item)
            where TRegistryStore : RegistryStore
        {
            if (GetType().Namespace != "Castle.Proxies")
                throw new InvalidOperationException("This method can only be called in a proxy instance.");

            _Item = item;
            _ColumnStores = columnStores.Where(x => x.Item == item);
            _ProxyLoaded = true;
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
