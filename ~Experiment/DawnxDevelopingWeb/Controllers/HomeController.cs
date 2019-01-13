using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DawnxDevelopingWeb.Models;
using Dawnx.AspNetCore.AppSupport;
using DawnxDevelopingWeb.Data;

namespace DawnxDevelopingWeb.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppRegistryManager<ApplicationDbContext, AppRegistryItem> _appRegistryManager;

        public HomeController(AppRegistryManager<ApplicationDbContext, AppRegistryItem> appRegistryManager)
        {
            _appRegistryManager = appRegistryManager;
        }

        public IActionResult Index()
        {
            using (_appRegistryManager.BeginAutoTransaction())
            {
                var item = _appRegistryManager.GetGlobalItem();
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
