using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dawnx.Algorithms.Tree;
using Dawnx.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace DawnxDevelopingWeb.Controllers
{
    public class StarAdminController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.TopActive = "/Reports";
            ViewBag.SidebarActive = "/Basic UI Elements/Buttons";

            return View();
        }

    }
}