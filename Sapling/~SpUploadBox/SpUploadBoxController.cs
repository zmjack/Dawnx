using Dawnx;
using Microsoft.AspNetCore.Mvc;

namespace Sapling
{
    public abstract class SpUploadBoxController : Controller
    {
        public virtual JsonResult Config()
        {
            return Json(JSend.Success.Create(new
            {
                statUrl = Url.Action(nameof(Stat)),
                previewUrl = Url.Action(nameof(Preview)),
                submitUrl = Url.Action(nameof(Submit)),
            }));
        }

        public abstract JsonResult Stat(string tag);
        public abstract IActionResult Preview(string tag);
        public abstract JsonResult Submit(string tag);
    }
}
