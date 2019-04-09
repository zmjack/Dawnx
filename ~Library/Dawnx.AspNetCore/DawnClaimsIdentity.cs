using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Dawnx.AspNetCore
{
    public static class DawnClaimsIdentity
    {
        /// <summary>
        /// Gets roles of the specified ClaimsPrincipal.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string[] GetRoles(this ClaimsIdentity @this) => GetClaims(@this, @this.RoleClaimType);

        /// <summary>
        /// Gets ID of the specified ClaimsPrincipal.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetId(this ClaimsIdentity @this) => GetClaim(@this, ClaimTypes.NameIdentifier);

        /// <summary>
        /// Gets claims of the specified cliam type of the specified ClaimsPrincipal.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static string[] GetClaims(this ClaimsIdentity @this, string claimType)
        {
            return @this.Claims
                .Where(x => x.Type == claimType)
                .Select(x => x.Value)
                .ToArray();
        }

        /// <summary>
        /// Gets the first claim of the specified cliam type of the specified ClaimsPrincipal.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static string GetClaim(this ClaimsIdentity @this, string claimType)
        {
            return @this.Claims
                .Where(x => x.Type == claimType)
                .Select(x => x.Value)
                .FirstOrDefault();
        }

    }
}
