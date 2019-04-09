using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Dawnx.AspNetCore;
using System.Linq;

namespace DawnxTemplate.Authorizations.UserAuthorize
{
    internal class UserAuthorizationHandler : AuthorizationHandler<UserAuthorizationRequirement>
    {
        private readonly ILogger<UserAuthorizationHandler> _logger;

        public UserAuthorizationHandler(ILogger<UserAuthorizationHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAuthorizationRequirement requirement)
        {
            if (requirement.Users.Contains(context.User.Identity.Name))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}