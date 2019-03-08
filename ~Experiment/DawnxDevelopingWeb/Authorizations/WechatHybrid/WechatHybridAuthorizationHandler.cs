using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Dawnx.AspNetCore;
using Dawnx.Utilities;

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
            _logger.LogWarning("Evaluating authorization requirement for age >= {age}", requirement.Age);

            var birthday = context.User.GetClaim(ClaimTypes.DateOfBirth);
            if (birthday != null)
            {
                var date = Convert.ToDateTime(birthday);
                var age = DateTimeUtility.GetCompleteYears(date, DateTime.Now);

                if (age >= requirement.Age)
                {
                    _logger.LogInformation($"Minimum age authorization requirement {requirement.Age} satisfied");
                    context.Succeed(requirement);
                }
                else _logger.LogInformation($"Current user's DateOfBirth claim ({birthday}) does not satisfy the minimum age authorization requirement {requirement.Age}");
            }
            else _logger.LogInformation("No DateOfBirth claim present");

            return Task.CompletedTask;
        }
    }
}