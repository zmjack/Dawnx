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