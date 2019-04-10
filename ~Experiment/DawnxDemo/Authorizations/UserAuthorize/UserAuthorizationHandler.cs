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
            var name = context.User.GetName(requirement.AuthenticationType);
            if (requirement.Users.Contains(name))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}