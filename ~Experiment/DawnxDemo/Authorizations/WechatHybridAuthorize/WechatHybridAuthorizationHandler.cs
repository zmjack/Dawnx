using Dawnx.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DawnxTemplate.Authorizations.WechatHybridAuthorize
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
            var openId = context.User.GetClaim(requirement.AuthenticationType, $"Wechat{nameof(WechatHybridUser.OpenId)}");
            if (openId != null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}