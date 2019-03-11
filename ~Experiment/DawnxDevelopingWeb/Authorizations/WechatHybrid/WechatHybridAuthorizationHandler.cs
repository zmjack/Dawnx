using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Dawnx.AspNetCore;

namespace DawnxDevelopingWeb.Authorizations.WechatHybrid
{
    internal class WechatHybridAuthorizationHandler : AuthorizationHandler<WechatHybridRequirement>
    {
        private readonly ILogger<WechatHybridAuthorizationHandler> _logger;

        public WechatHybridAuthorizationHandler(ILogger<WechatHybridAuthorizationHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WechatHybridRequirement requirement)
        {
            var openId = context.User.GetClaim($"Wechat{nameof(WechatHybridUser.OpenId)}");
            if (openId != null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}