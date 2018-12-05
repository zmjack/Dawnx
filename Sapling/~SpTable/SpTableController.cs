using Dawnx;
using Microsoft.AspNetCore.Mvc;

namespace Sapling
{
    public abstract class SpTableController : Controller
    {
        public virtual JsonResult Config()
        {
            return Json(JSend.Success.Create(new
            {
                sourceUrl = Url.Action(nameof(Source)),
            }));
        }

        public abstract JsonResult Source(string tag, int page);
    }
}
