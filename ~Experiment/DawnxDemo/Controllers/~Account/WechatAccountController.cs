using Dawnx;
using DawnxTemplate.Authorizations.WechatHybridAuthorize;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DawnxDemo.Controllers
{
    public class WechatAccountController : Controller
    {
        public RedirectResult SignIn(string returnUrl)
        {
            /* 尝试去企业号获取用户信息
             * 回调至
             * Url.Action(nameof(Authorize), "WechatAccount", new
               {
                   openIdType = WechatHybridOpenIdType.Enterprise,
                   returnUrl = Request.Url()
               })
             */
            return Redirect(Url.Action(nameof(Authorize), new
            {
                openIdType = WechatHybridOpenIdType.Enterprise,
                returnUrl = returnUrl,
            }));
        }

        public IActionResult Authorize(WechatHybridOpenIdType openIdType, string code, string state, string returnUrl)
        {
            switch (openIdType)
            {
                case WechatHybridOpenIdType.Enterprise:
                    try
                    {
                        HttpContext.SignInAsync(new ClaimsPrincipal(new WechatHybridUser
                        {
                            OpenIdType = WechatHybridOpenIdType.Enterprise,
                            OpenId = "(OpenId)",
                            EntUserName = "(EntUserName)",
                        }.ToIdentity())).Wait();

                        return Redirect(returnUrl);
                    }
                    catch
                    {
                        return Content("Error.");
                    }

                case WechatHybridOpenIdType.Public:
                    try
                    {
                        HttpContext.SignInAsync(new ClaimsPrincipal(new WechatHybridUser
                        {
                            OpenIdType = WechatHybridOpenIdType.Public,
                            OpenId = "(OpenId)",
                            PubUserName = "(PubUserName)",
                        }.ToIdentity())).Wait();

                        return Redirect(returnUrl);
                    }
                    catch
                    {
                        return Content("Error.");
                    }

                default: return Json(JSend.Error.Create("Unknown authenticate method."));
            }
        }

        public IActionResult Denied() => Content("Denied.");

    }
}