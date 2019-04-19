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
                @this.Cookies.Delete(key);
        }

        public static void ClearCookies(this HttpResponse @this, IRequestCookieCollection cookies, Action<CookieOptions> setOptions)
        {
            var cookieOptions = new CookieOptions();
            setOptions(cookieOptions);

            foreach (var key in cookies.Keys)
                @this.Cookies.Delete(key, cookieOptions);
        }

        public static void PurgeCookies(this HttpResponse @this, IRequestCookieCollection cookies) => ClearCookies(@this, cookies, x => x.Path = "");

    }
}
