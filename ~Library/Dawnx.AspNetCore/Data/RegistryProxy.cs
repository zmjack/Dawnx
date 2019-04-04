using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dawnx.Data
{
    public class RegistryProxy<TRegistry> : IInterceptor
       where TRegistry : Registry
    {
        public void Intercept(IInvocation invocation)
        {
            var proxy = (TRegistry)invocation.Proxy;

            string property;
            string value;
            RegistryStore store;
            PropertyInfo proxyProperty;

            if (proxy.ProxyLoaded)
            {
                switch (invocation.Method.Name)
                {
                    case string name when name == $"set_{nameof(Registry.Item)}": goto default;
                    case string name when name == $"get_{nameof(Registry.Item)}": goto default;

                    case string name when name.StartsWith("set_"):
                        property = invocation.Method.Name.Substring(4);
                        value = invocation.Arguments[0].ToString();
                        store = proxy.ColumnStores.FirstOrDefault(x => x.Key == property);
                        proxyProperty = proxy.GetType().GetProperty(property);

                        if (store != null)
                            store.Value = value.ToString();
                        else throw new KeyNotFoundException();
                        break;

                    case string name when name.StartsWith("get_"):
                        property = invocation.Method.Name.Substring(4);
                        store = proxy.ColumnStores.FirstOrDefault(x => x.Key == property);
                        proxyProperty = proxy.GetType().GetProperty(property);

                        if (store != null)
                            invocation.ReturnValue = Convert.ChangeType(store.Value, proxyProperty.PropertyType);
                        else invocation.Proceed();
                        break;

                    default: invocation.Proceed(); break;
                }
            }
            else invocation.Proceed();
        }
    }

}
