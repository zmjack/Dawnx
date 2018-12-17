using System;
using System.Linq;
using System.Security.Claims;

namespace Dawnx.AspNetCore
{
    public class Authority
    {
        public static UnauthorizedAccessException New_UnauthorizedAccessException =>
            new UnauthorizedAccessException("The current user is not allowed to access this resource.");

        public bool AllowAnonymous = false;
        public string[] AllowUsers = new string[0];
        public string[] AllowRoles = new string[0];

        public bool IsUserAllowed(ClaimsPrincipal user)
        {
            if (AllowAnonymous
                || AllowUsers.Contains(user.Identity.Name)
                || AllowRoles.Any(role => user.GetRoles().Contains(role)))
                return true;
            else return false;
        }
    }
}
