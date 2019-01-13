using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LiveManagerService
    {
        public static Type ServiceType { get; private set; }

        public static void AddLiveAccount<TDbContext>(this IServiceCollection @this)
            where TDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>, ILiveAccountDbContext
        {
            @this.AddScoped<LiveManager<TDbContext>>();
            ServiceType = typeof(LiveManager<TDbContext>);
        }

        public static void AddLiveAccount<TDbContext, TUser>(this IServiceCollection @this)
            where TDbContext : IdentityDbContext<TUser, IdentityRole, string>, ILiveAccountDbContext
            where TUser : IdentityUser
        {
            @this.AddScoped<LiveManager<TDbContext, TUser>>();
            ServiceType = typeof(LiveManager<TDbContext, TUser>);
        }

    }
}
