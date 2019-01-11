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
        public static string GetAccessToken(this HttpContext @this) => @this.GetTokenAsync("access_token").Result;

        public static string GetRefreshToken(this HttpContext @this) => @this.GetTokenAsync("refresh_token").Result;

    }
}
