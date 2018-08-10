using System;
using System.Linq;
using System.Security.Claims;

namespace Dawnx.AspNetCore.IdentityUtility
{
    public static class IdentityUtility
    {
        public static UnauthorizedAccessException New_UnauthorizedAccessException =>
            new UnauthorizedAccessException("The current user is not allowed to access this resource.");

        public static bool AllowAnonymous = false;
        public static string[] AllowRoles = new string[0];
        public static string[] AllowUsers = new string[0];

        public static bool IsUserAllowed(ClaimsPrincipal user)
        {
            if (AllowAnonymous
                || AllowUsers.Contains(user.Identity.Name)
                || AllowRoles.Any(role => user.GetRoles().Contains(role)))
                return true;
            else return false;
        }

        public static class Advanced
        {
            public static bool AllowAnonymous = false;
            public static string[] AllowRoles = new string[0];
            public static string[] AllowUsers = new string[0];

            public static bool IsUserAllowed(ClaimsPrincipal user)
            {
                if (AllowAnonymous
                    || AllowUsers.Contains(user.Identity.Name)
                    || AllowRoles.Any(role => user.GetRoles().Contains(role)))
                    return true;
                else return false;
            }
        }

    }
}
