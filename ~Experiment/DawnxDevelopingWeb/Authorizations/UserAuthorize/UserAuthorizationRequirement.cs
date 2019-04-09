using Dawnx.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace DawnxTemplate.Authorizations.UserAuthorize
{
    internal class UserAuthorizationRequirement : AuthorizationRequirementBase
    {
        public UserAuthorizationRequirement(string authenticationType) : base(authenticationType)
        {
        }

        public string[] Users { get; set; }

    }
}