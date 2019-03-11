using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DawnxDevelopingWeb.Models;
using Dawnx.AspNetCore.AppSupport;
using DawnxDevelopingWeb.Data;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using DawnxDevelopingWeb.Authorizations.MinimumAge;

namespace DawnxDevelopingWeb.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppRegistryManager<ApplicationDbContext, AppRegistryItem> _appRegistryManager;

        public HomeController(AppRegistryManager<ApplicationDbContext, AppRegistryItem> appRegistryManager)
        {
            _appRegistryManager = appRegistryManager;
        }

        public async Task<IActionResult> SignInAsync(DateTime birthday)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "Specified User"),
                new Claim(ClaimTypes.DateOfBirth, birthday.ToShortDateString())
            }, "UserSpecified");

            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return RedirectToAction(nameof(Index));
        }

        [MinimumAgeAuthorize(2)]
        public IActionResult Index()
        {
            using (_appRegistryManager.BeginAutoTransaction())
            {
                var item = _appRegistryManager.GetGlobalItem();
                item.AllowedCount = 5;
                _appRegistryManager.SaveChanges();
            }

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
