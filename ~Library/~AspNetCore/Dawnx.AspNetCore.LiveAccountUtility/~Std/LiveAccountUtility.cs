using System;
using System.Linq;
using System.Security.Claims;

namespace Dawnx.AspNetCore.LiveAccountUtility
{
    public static class LiveAccountUtility
    {
        public static AuthorityUtility NormalControlPanel = new AuthorityUtility();
        public static AuthorityUtility RoleAndOperationControlPanel = new AuthorityUtility();
    }
}
