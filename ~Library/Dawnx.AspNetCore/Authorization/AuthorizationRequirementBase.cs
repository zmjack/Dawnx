using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

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
