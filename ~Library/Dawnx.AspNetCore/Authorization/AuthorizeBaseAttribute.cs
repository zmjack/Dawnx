using Microsoft.AspNetCore.Authorization;

namespace Dawnx.AspNetCore.Authorization
{
    public abstract class AuthorizeBaseAttribute : AuthorizeAttribute
    {
        public abstract string PolicyPrefix { get; }
        public string AuthenticationType { get; set; }

        public AuthorizeBaseAttribute(string authenticationType)
        {
            AuthenticationType = authenticationType;
        }
    }

}
