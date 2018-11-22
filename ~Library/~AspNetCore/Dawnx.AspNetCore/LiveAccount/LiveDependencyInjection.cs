using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dawnx.AspNetCore.LiveAccount
{
    public static class LiveDependencyInjection
    {
        public static Type LiveAccountService { get; private set; }

        public static void AddLiveAccount<TDbContext>(this IServiceCollection @this)
            where TDbContext : IdentityDbContext, ILiveDbContext
        {
            @this.AddScoped<LiveManager<TDbContext>>();
            LiveAccountService = typeof(LiveManager<TDbContext>);
        }

    }
}
