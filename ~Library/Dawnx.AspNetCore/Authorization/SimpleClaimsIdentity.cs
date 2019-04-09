using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Dawnx.AspNetCore.Authorization
{
    public class SimpleClaimsIdentity : ClaimsIdentity
    {
        public SimpleClaimsIdentity(string userName, string[] roles)
        {
            AddClaim(new Claim(NameClaimType, userName));

            if (roles != null)
                AddClaims(roles.Select(x => new Claim(RoleClaimType, x)));
        }

    }
}
