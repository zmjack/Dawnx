using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DawnxDevelopingWeb.Models;
using DawnxDevelopingWeb.Data;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using DawnxTemplate.Authorizations.WechatHybridAuthorize;

namespace DawnxDevelopingWeb.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public async Task<IActionResult> SignInAsync(DateTime birthday)
        {
            var identity = new WechatHybridUser
            {
                OpenIdType = WechatHybridOpenIdType.Public,
                OpenId = Guid.NewGuid().ToString(),
                PubUserName = "",
            }.ToIdentity();

            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return RedirectToAction(nameof(Index));
        }

        //[WechatHybridAuthorize]
        public IActionResult Index()
        {
            return View();
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
