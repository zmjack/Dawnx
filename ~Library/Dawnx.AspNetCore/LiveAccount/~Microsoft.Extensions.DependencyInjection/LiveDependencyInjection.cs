using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LiveAccountExtensions
    {
        public static Type LiveAccountService { get; private set; }

        public static void AddLiveAccount<TDbContext>(this IServiceCollection @this)
            where TDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>, ILiveAccountDbContext
        {
            @this.AddScoped<LiveManager<TDbContext>>();
            LiveAccountService = typeof(LiveManager<TDbContext>);
        }

        public static void AddLiveAccount<TDbContext, TUser>(this IServiceCollection @this)
            where TDbContext : IdentityDbContext<TUser, IdentityRole, string>, ILiveAccountDbContext
            where TUser : IdentityUser
        {
            @this.AddScoped<LiveManager<TDbContext, TUser>>();
            LiveAccountService = typeof(LiveManager<TDbContext, TUser>);
        }

    }
}
