using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Dawnx.AspNetCore
{
    public static class DawnClaimsPrincipal
    {
        /// <summary>
        /// Gets user's menu from the specified menu.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="this"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        public static ClaimsMenu<TModel> Menu<TModel>(this ClaimsPrincipal @this, ClaimsMenu<TModel> menu)
            where TModel : INameable, IClaimsPermission, new()
            => menu.UserMenu(@this);

        /// <summary>
        /// Gets roles of the specified ClaimsPrincipal.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="authenticationType">If null, then use the default authenticationType.</param>
        /// <returns></returns>
        public static string GetName(this ClaimsPrincipal @this, string authenticationType = null)
        {
            ClaimsIdentity identity;
            if (authenticationType != null)
                identity = @this.Identities.FirstOrDefault(x => x.AuthenticationType == authenticationType);
            else identity = @this.Identity as ClaimsIdentity;

            if (identity != null)
                return DawnClaimsIdentity.GetName(identity);
            else return null;
        }

        /// <summary>
        /// Gets roles of the specified ClaimsPrincipal.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="authenticationType">If null, then use the default authenticationType.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetRoles(this ClaimsPrincipal @this, string authenticationType = null)
        {
            ClaimsIdentity identity;
            if (authenticationType != null)
                identity = @this.Identities.FirstOrDefault(x => x.AuthenticationType == authenticationType);
            else identity = @this.Identity as ClaimsIdentity;

            if (identity != null)
                return DawnClaimsIdentity.GetRoles(identity);
            else return new string[0];
        }

        /// <summary>
        /// Gets ID of the specified ClaimsPrincipal.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="authenticationType">If null, then use the default authenticationType.</param>
        /// <returns></returns>
        public static string GetId(this ClaimsPrincipal @this, string authenticationType = null) => GetClaim(@this, authenticationType, ClaimTypes.NameIdentifier);

        /// <summary>
        /// Gets claims of the specified cliam type of the specified ClaimsPrincipal.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="authenticationType">If null, then use the default authenticationType.</param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetClaims(this ClaimsPrincipal @this, string authenticationType, string claimType)
        {
            ClaimsIdentity identity;
            if (authenticationType != null)
                identity = @this.Identities.FirstOrDefault(x => x.AuthenticationType == authenticationType);
            else identity = @this.Identity as ClaimsIdentity;

            if (identity != null)
                return DawnClaimsIdentity.GetClaims(identity, claimType);
            else return new string[0];
        }
        /// <summary>
        /// Gets the first claim of the specified cliam type of the specified ClaimsPrincipal.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="authenticationType">If null, then use the default authenticationType.</param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static string GetClaim(this ClaimsPrincipal @this, string authenticationType, string claimType) => GetClaims(@this, authenticationType, claimType).FirstOrDefault();

    }
}
