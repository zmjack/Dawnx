using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Actions
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILiveAccountManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveAccountManager>(LiveAccountDependencyInjection.LiveAccountService);
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (!LiveAccountUtility.IsUserAllowed(User)) throw LiveAccountUtility.New_UnauthorizedAccessException;

            if (HttpContext.Request.Path.ToString().EndsWith("Actions/"))
                return Redirect("Index");
            if (HttpContext.Request.Path.ToString().EndsWith("Actions"))
                return Redirect("Actions/Index");

            ViewData["Items"] = _liveAccountManager.LiveActions.ToArray();
            return Page();
        }

    }
}