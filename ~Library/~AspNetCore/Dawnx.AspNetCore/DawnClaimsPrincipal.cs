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
            var roleClaimType = (@this.Identity as ClaimsIdentity)?.RoleClaimType ?? ClaimTypes.Role;

            return from claim in @this.Claims
                   where claim.Type == roleClaimType
                   select claim.Value;
        }

    }
}
