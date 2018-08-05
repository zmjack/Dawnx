using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dawnx.AspNetCore.LiveAccount
{
    public static class LiveAccountDependencyInjection
    {
        public static Type LiveAccountService { get; private set; }

        public static void AddLiveAccount<TDbContext>(this IServiceCollection @this)
            where TDbContext : IdentityDbContext, ILiveAccount
        {
            @this.AddScoped<LiveAccountManager<TDbContext>>();
            LiveAccountService = typeof(LiveAccountManager<TDbContext>);
        }

    }
}
