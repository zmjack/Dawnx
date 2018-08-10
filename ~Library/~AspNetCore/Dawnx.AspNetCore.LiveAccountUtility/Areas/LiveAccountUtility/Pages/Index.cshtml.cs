using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages
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
            if (!LiveAccountUtility.IsUserAllowed(User))
                throw LiveAccountUtility.New_UnauthorizedAccessException;

            if (HttpContext.Request.Path.ToString().EndsWith("LiveAccountUtility/"))
                return Redirect("Index");
            if (HttpContext.Request.Path.ToString().EndsWith("LiveAccountUtility"))
                return Redirect("LiveAccountUtility/Index");

            return Page();
        }

    }
}