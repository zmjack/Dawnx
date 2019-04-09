using Dawnx;
using Dawnx.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace DawnxTemplate.Authorizations.UserAuthorize
{
    public class UserAuthorizeAttribute : SchemaAuthorizeAttribute
    {
        public override string PolicyPrefix => nameof(UserAuthorize);

        public UserAuthorizeAttribute(
            string[] users,
            string schema = CookieAuthenticationDefaults.AuthenticationScheme) : base(schema)
        {
            Policy = $@"{PolicyPrefix} ""{users.Join("\t")}""";

            if (schema != null)
                Policy += $"--schema {AuthenticationScheme}";
        }

    }
}