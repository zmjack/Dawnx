using Microsoft.AspNetCore.Authorization;

namespace Dawnx.AspNetCore.Authorization
{
    public abstract class AuthorizationRequirementBase : IAuthorizationRequirement
    {
        public string AuthenticationType { get; set; }

        public AuthorizationRequirementBase(string authenticationType)
        {
            this.AuthenticationType = authenticationType;
        }
    }

}
