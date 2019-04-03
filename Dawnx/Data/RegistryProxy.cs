using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dawnx.Data
{
    public class RegistryProxy<TColumnStorable, TIColumnStore> : IInterceptor
       where TColumnStorable : IColumnStorable<TIColumnStore>
       where TIColumnStore : IColumnStore
    {
        private static string EntityKeyProperty;

        static RegistryProxy()
        {
            EntityKeyProperty = typeof(TColumnStorable).GetProperties()
                .First(x => x.GetCustomAttribute<KeyAttribute>() != null).Name;
        }

        public void Intercept(IInvocation invocation)
        {
            var proxy = (IColumnStorable<TIColumnStore>)invocation.Proxy;

            string property;
            string value;
            TIColumnStore store;
            PropertyInfo proxyProperty;

            if (proxy.ProxyLoaded)
            {
                switch (invocation.Method.Name)
                {
                    case string name when name == $"set_{EntityKeyProperty}": goto default;
                    case string name when name == $"get_{EntityKeyProperty}": goto default;

                    case string name when name.StartsWith("set_"):
                        property = invocation.Method.Name.Substring(4);
                        value = invocation.Arguments[0].ToString();
                        store = proxy.ColumnStores.FirstOrDefault(x => x.Key == property);
                        proxyProperty = proxy.GetType().GetProperty(property);

                        if (store != null)
                            store.Value = value.ToString();
                        break;

                    case string name when name.StartsWith("get_"):
                        property = invocation.Method.Name.Substring(4);
                        store = proxy.ColumnStores.FirstOrDefault(x => x.Key == property);
                        proxyProperty = proxy.GetType().GetProperty(property);

                        if (store != null)
                            invocation.ReturnValue = Convert.ChangeType(store.Value, proxyProperty.PropertyType);
                        break;

                    default: invocation.Proceed(); break;
                }
            }
            else invocation.Proceed();
        }
    }

}
