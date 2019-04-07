using Microsoft.AspNetCore.Mvc;

namespace VuetsSimple.Controllers
{
    public class VuetsController : Controller
    {
        public ViewResult Index() => View();
        public ViewResult VueRender() => View();
        public ViewResult TsRender() => View();
    }
}
