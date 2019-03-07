using Dawnx.AspNetCore.AppSupport;
using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore;
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
