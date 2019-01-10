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
        /// <returns></returns>
        public static string[] GetClaim(this IEnumerable<Claim> @this)
            => @this.Where(x => x.Type == "sub").Select(x => x.Value).ToArray();

        /// <summary>
        /// Determines if reqeust is sent from `sub`.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsContainsSubject(this IEnumerable<Claim> @this) => @this.Any(x => x.Type == "sub");

    }
}
