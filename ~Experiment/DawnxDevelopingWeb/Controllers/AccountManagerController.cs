using Microsoft.AspNetCore.Mvc;

namespace DawnxDevelopingWeb.Controllers
{
    public class AccountManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}