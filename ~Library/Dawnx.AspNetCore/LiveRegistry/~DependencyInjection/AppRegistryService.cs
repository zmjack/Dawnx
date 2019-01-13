using Dawnx.AspNetCore.LiveRegistry;
using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppRegistryManageService
    {
        public static Type ServiceType { get; private set; }

        public static void AddAppRegistry<TDbContext, TAppRegistryItem>(this IServiceCollection @this)
            where TDbContext : DbContext, IAppRegistryDbContext
            where TAppRegistryItem : class, new()
        {
            @this.AddScoped<AppRegistryManager<TDbContext, TAppRegistryItem>>();
            ServiceType = typeof(AppRegistryManager<TDbContext, TAppRegistryItem>);
        }

    }
}
