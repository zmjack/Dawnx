using System;
using Dawnx.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RewriteAuthorizationProviderService
    {
        public static void RewriteAuthorizationProvider<TAuthorizationPolicyProvider>(this IServiceCollection @this)
            where TAuthorizationPolicyProvider : CustomExtraAuthorizationPolicyProvider, new()
        {
            @this.AddSingleton<IAuthorizationPolicyProvider, TAuthorizationPolicyProvider>();

            foreach (var handlerType in new TAuthorizationPolicyProvider().RequirementHandlerTypes)
                @this.AddSingleton(typeof(IAuthorizationHandler), handlerType);
        }

    }
}
