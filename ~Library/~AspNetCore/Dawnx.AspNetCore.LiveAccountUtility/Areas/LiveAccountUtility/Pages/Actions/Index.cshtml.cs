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
        private readonly ILiveManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveManager>(LiveDependencyInjection.LiveAccountService);
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (!LiveAccountUtility.Authority?.Advanced?.IsUserAllowed(User) ?? false) throw Authority.New_UnauthorizedAccessException;

            if (HttpContext.Request.Path.ToString().EndsWith("Actions/"))
                return Redirect("Index");
            if (HttpContext.Request.Path.ToString().EndsWith("Actions"))
                return Redirect("Actions/Index");

            ViewData["Items"] = _liveAccountManager.LiveActions.ToArray();
            return Page();
        }

    }
}