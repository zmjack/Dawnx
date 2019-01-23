using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dawnx.AspNetCore
{
    public static class DawnHttpContext
    {
        /// <summary>
        /// Same as: GetTokenAsync("access_token").Result;
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetAccessToken(this HttpContext @this) => @this.GetTokenAsync("access_token").Result;

        /// <summary>
        /// Same as: GetTokenAsync("refresh_token").Result;
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetRefreshToken(this HttpContext @this) => @this.GetTokenAsync("refresh_token").Result;

    }
}
