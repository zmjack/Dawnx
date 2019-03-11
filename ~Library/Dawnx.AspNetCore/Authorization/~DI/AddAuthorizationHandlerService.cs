using System;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AddAuthorizationHandlerService
    {
        public static Type ServiceType { get; private set; }

        public static void AddAuthorizationHandler<TAuthorizationHandler>(this IServiceCollection @this)
            where TAuthorizationHandler : class, IAuthorizationHandler
        {
            @this.AddSingleton<IAuthorizationHandler, TAuthorizationHandler>();
        }

    }
}
