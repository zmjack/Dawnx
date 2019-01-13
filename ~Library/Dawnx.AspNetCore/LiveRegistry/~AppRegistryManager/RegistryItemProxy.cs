using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.AspNetCore.LiveRegistry
{
    public class RegistryItemProxy<TDbContext, TAppRegistryItem> : IInterceptor
        where TDbContext : DbContext, IAppRegistryDbContext
        where TAppRegistryItem : class, new()
    {
        private readonly AppRegistryManager<TDbContext, TAppRegistryItem> _manager;

        public RegistryItemProxy(AppRegistryManager<TDbContext, TAppRegistryItem> manager)
        {
            _manager = manager;
        }

        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("Pre DoSomething");
            invocation.Proceed();
            Console.WriteLine("Post DoSomething");
        }
    }

}
