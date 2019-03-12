﻿using Dawnx.AspNetCore.AppSupport;
using System;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppRegistryService
    {
        public static Type ServiceType { get; private set; }

        public static void AddAppRegistry<TDbContext, TAppRegistryItem>(this IServiceCollection @this)
            where TDbContext : DbContext, IAppRegistryDbContext
            where TAppRegistryItem : class, IAppRegistryItem, new()
        {
            @this.AddScoped<AppRegistryManager<TDbContext, TAppRegistryItem>>();
            ServiceType = typeof(AppRegistryManager<TDbContext, TAppRegistryItem>);
        }

    }
}