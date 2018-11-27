using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dawnx.AspNetCore.LiveAccount
{
    public class LiveAuthorizeAttribute : ActionFilterAttribute
    {
        public LiveAuthorizeAttribute()
        {
            Order = 100;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var liveAccountService = DIUtility.GetEntryService<ILiveManager>(LiveAccountExtensions.LiveAccountService);

            if (!liveAccountService.CheckAuthorization(context))
                throw new UnauthorizedAccessException($"LiveAccount authorize faild.");
        }
    }
}
