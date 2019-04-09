using Dawnx.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace DawnxTemplate.Authorizations.UserAuthorize
{
    internal class UserAuthorizationRequirement : SchemaAuthorizationRequirement
    {
        public UserAuthorizationRequirement(string schema) : base(schema)
        {
        }

        public string[] Users { get; set; }

    }
}