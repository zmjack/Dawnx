using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore;

namespace Dawnx.AspNetCore.AppSupport
{
    public class RegistryItemProxy<TDbContext, TAppRegistryItem> : IInterceptor
        where TDbContext : DbContext, IAppRegistryDbContext
        where TAppRegistryItem : class, IAppRegistryItem, new()
    {
        private readonly AppRegistryManager<TDbContext, TAppRegistryItem> _manager;

        public RegistryItemProxy(AppRegistryManager<TDbContext, TAppRegistryItem> manager)
        {
            _manager = manager;
        }

        public void Intercept(IInvocation invocation)
        {
            switch (invocation.Method.Name)
            {
                case string name when name.StartsWith("set_"):
                    _manager.SetGlobalItemValue(invocation.Method.Name.Substring(4), invocation.Arguments[0].ToString());
                    invocation.Proceed();
                    break;

                default: invocation.Proceed(); break;
            }
        }
    }

}
