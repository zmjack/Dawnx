using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.IdentityUtility.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Request.Path.ToString().EndsWith("IdentityUtility/"))
                return Redirect("Index");
            if (HttpContext.Request.Path.ToString().EndsWith("IdentityUtility"))
                return Redirect("IdentityUtility/Index");

            return Page();
        }

    }
}