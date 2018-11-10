using System;
using System.Linq;
using System.Security.Claims;

namespace Dawnx.AspNetCore.IdentityUtility
{
    public static class IdentityUtility
    {
        public static AuthorityUtility UserControlPanel = new AuthorityUtility();
        public static AuthorityUtility RoleControlPanel = new AuthorityUtility();
    }
}
