using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Dawnx.AspNetCore;

namespace CustomPolicyProvider
{
    internal class MinimumAgeAuthorizationHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeAuthorizationHandler> _logger;

        public MinimumAgeAuthorizationHandler(ILogger<MinimumAgeAuthorizationHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            _logger.LogWarning("Evaluating authorization requirement for age >= {age}", requirement.Age);

            var birthday = context.User.GetClaim(ClaimTypes.DateOfBirth);
            if (birthday != null)
            {
                var date = Convert.ToDateTime(birthday);

                var age = DateTime.Now.Year - date.Year;
                if (date > DateTime.Now.AddYears(-age))
                {
                    age--;
                }

                if (age >= requirement.Age)
                {
                    _logger.LogInformation("Minimum age authorization requirement {age} satisfied", requirement.Age);
                    context.Succeed(requirement);
                }
                else
                {
                    _logger.LogInformation("Current user's DateOfBirth claim ({dateOfBirth}) does not satisfy the minimum age authorization requirement {age}",
                        birthday,
                        requirement.Age);
                }
            }
            else
            {
                _logger.LogInformation("No DateOfBirth claim present");
            }

            return Task.CompletedTask;
        }
    }
}