using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Dawnx.AspNetCore
{
    public static class DawnClaimsPrincipal
    {
        /// <summary>
        /// Returns user's menu from the specified menu.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="this"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        public static ClaimsMenu<TModel> Menu<TModel>(this ClaimsPrincipal @this, ClaimsMenu<TModel> menu)
            where TModel : INameable, IClaimsPermission, new()
            => menu.UserMenu(@this);

        /// <summary>
        /// Returns roles of the specified ClaimsPrincipal.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetRoles(this ClaimsPrincipal @this)
        {
            var claimType = (@this.Identity as ClaimsIdentity)?.RoleClaimType ?? ClaimTypes.Role;
            return @this.Claims
                .Where(x => x.Type == claimType)
                .Select(x => x.Value);
        }

        /// <summary>
        /// Returns ID of the specified ClaimsPrincipal.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetId(this ClaimsPrincipal @this)
        {
            var claimType = ClaimTypes.NameIdentifier;
            return @this.Claims
                .Where(x => x.Type == claimType)
                .Select(x => x.Value)
                .First();
        }

    }
}
