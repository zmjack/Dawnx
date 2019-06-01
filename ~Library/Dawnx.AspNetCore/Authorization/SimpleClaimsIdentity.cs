using System.Linq;
using System.Security.Claims;

namespace Dawnx.AspNetCore.Authorization
{
    public class SimpleClaimsIdentity : ClaimsIdentity
    {
        public SimpleClaimsIdentity(string scheme, string userName, string[] roles)
            : base(scheme, DefaultNameClaimType, DefaultRoleClaimType)
        {
            AddClaim(new Claim(DefaultNameClaimType, userName));

            if (roles != null)
                AddClaims(roles.Select(x => new Claim(DefaultRoleClaimType, x)));
        }

        public SimpleClaimsIdentity(string userName, string[] roles)
        {
            AddClaim(new Claim(NameClaimType, userName));

            if (roles != null)
                AddClaims(roles.Select(x => new Claim(RoleClaimType, x)));
        }

    }
}
