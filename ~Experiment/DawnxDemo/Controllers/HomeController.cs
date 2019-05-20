using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DawnxDemo.Models;
using DawnxDemo.Data;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using DawnxTemplate.Authorizations.WechatHybridAuthorize;
using Dawnx.AspNetCore.Authorization;
using DawnxTemplate.Authorizations.UserAuthorize;

namespace DawnxDemo.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            throw new Exception("123");
            return View();
        }

        public async Task<IActionResult> SignInWechatHybrid()
        {
            var identity = new WechatHybridUser
            {
                OpenIdType = WechatHybridOpenIdType.Public,
                OpenId = Guid.NewGuid().ToString(),
                PubUserName = "",
            }.ToIdentity();

            await HttpContext.SignInAsync("scheme1", new ClaimsPrincipal(identity));

            return RedirectToAction(nameof(Scheme1));
        }

        [WechatHybridAuthorize(AuthenticationSchemes = "scheme1")]
        public IActionResult Scheme1()
        {
            return Content("OK");
        }

        public async Task<IActionResult> SignInSimpleId(DateTime birthday)
        {
            var identity = new SimpleClaimsIdentity("SimpleId", new[] { "Beginner", "Display" });

            await HttpContext.SignInAsync("scheme2", new ClaimsPrincipal(identity));

            return RedirectToAction(nameof(Scheme2));
        }

        [UserAuthorize(new[] { "SimpleId" }, AuthenticationSchemes = "scheme2")]
        public IActionResult Scheme2()
        {
            return Content("OK");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
