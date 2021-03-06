﻿using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace Dawnx.AspNetCore
{
    public static class DawnIAntiforgery
    {
        public static string AntiForgeryToken(this IAntiforgery @this, HttpContext httpContext)
            => @this.GetAndStoreTokens(httpContext).RequestToken;
    }
}
