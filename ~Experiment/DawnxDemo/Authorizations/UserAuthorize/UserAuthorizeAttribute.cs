using Dawnx;
using Dawnx.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace DawnxTemplate.Authorizations.UserAuthorize
{
    public class UserAuthorizeAttribute : AuthorizeBaseAttribute
    {
        public override string PolicyPrefix => nameof(UserAuthorize);

        public UserAuthorizeAttribute(
            string[] users,
            string authenticationType = null) : base(authenticationType)
        {
            Policy = $@"{PolicyPrefix} ""{users.Join("\t")}""";

            if (authenticationType != null)
                Policy += $"--type {AuthenticationType}";
        }

    }
}