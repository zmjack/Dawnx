using Dawnx.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
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

        public static void Login(this HttpContext @this, string userName, string[] roles = null) => LoginAsync(@this, userName, roles).Wait();
        public static async Task LoginAsync(this HttpContext @this, string userName, string[] roles = null)
            => await @this.SignInAsync(new ClaimsPrincipal(new SimpleClaimsIdentity(userName, roles)));

        public static void Login(this HttpContext @this, string scheme, string userName, string[] roles = null) => LoginAsync(@this, scheme, userName, roles).Wait();
        public static async Task LoginAsync(this HttpContext @this, string scheme, string userName, string[] roles = null)
            => await @this.SignInAsync(scheme, new ClaimsPrincipal(new SimpleClaimsIdentity(userName, roles)));

        public static void Logout(this HttpContext @this) => LogoutAsync(@this).Wait();
        public static async Task LogoutAsync(this HttpContext @this) => await @this.SignOutAsync();

        public static void Logout(this HttpContext @this, string shceme) => LogoutAsync(@this, shceme).Wait();
        public static async Task LogoutAsync(this HttpContext @this, string shceme) => await @this.SignOutAsync(shceme);

    }
}
