using Dawnx.AspNetCore.AppSupport;
using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RewriteAuthorizationProviderService
    {
        public static Type ServiceType { get; private set; }

        public static void RewriteAuthorizationProvider<TAuthorizationPolicyProvider>(this IServiceCollection @this)
            where TAuthorizationPolicyProvider : class, IAuthorizationPolicyProvider
        {
            @this.AddSingleton<IAuthorizationPolicyProvider, TAuthorizationPolicyProvider>();
        }

    }
}
