using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Dawnx.AspNetCore
{
    public static class DawnClaimArray
    {
        /// <summary>
        /// Gets the value of `sub`. If the claims don't contains it, return null.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string[] GetClaims(this IEnumerable<Claim> @this, string type)
            => @this.Where(x => x.Type == type).Select(x => x.Value).ToArray();

    }
}
