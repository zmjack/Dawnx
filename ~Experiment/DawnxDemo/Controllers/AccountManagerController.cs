using Microsoft.AspNetCore.Mvc;

namespace DawnxDemo.Controllers
{
    public class AccountManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}