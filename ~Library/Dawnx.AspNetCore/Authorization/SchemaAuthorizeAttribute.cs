using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.AspNetCore.Authorization
{
    public abstract class SchemaAuthorizeAttribute : AuthorizeAttribute
    {
        public abstract string PolicyPrefix { get; }
        public string AuthenticationScheme { get; set; }

        public SchemaAuthorizeAttribute(string schema)
        {
            AuthenticationScheme = schema;
        }
    }

}
