using Dawnx.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.AspNetCore
{
    public static class DawnHttpResponse
    {
        public static void ClearCookies(this HttpResponse @this, IRequestCookieCollection cookies)
        {
            foreach (var key in cookies.Keys)
            {
                @this.Cookies.Append(key, "", new CookieOptions
                {
                    Expires = DateTimeUtility.UnixMinValue(),
                });
            }
        }

    }
}
