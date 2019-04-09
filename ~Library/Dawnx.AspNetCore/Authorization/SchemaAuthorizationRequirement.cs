using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.AspNetCore.Authorization
{
    public abstract class SchemaAuthorizationRequirement : IAuthorizationRequirement
    {
        public string AuthenticationScheme { get; set; }

        public SchemaAuthorizationRequirement(string schema)
        {
            AuthenticationScheme = schema;
        }
    }

}
