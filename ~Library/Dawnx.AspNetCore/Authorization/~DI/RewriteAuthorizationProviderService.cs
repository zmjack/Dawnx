using System;
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
