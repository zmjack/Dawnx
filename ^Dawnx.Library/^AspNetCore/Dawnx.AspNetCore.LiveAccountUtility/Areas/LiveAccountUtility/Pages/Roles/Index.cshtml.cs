using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Roles
{
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
            if (!LiveAccountUtility.IsUserAllowed(User))
                throw LiveAccountUtility.New_UnauthorizedAccessException;

            if (HttpContext.Request.Path.ToString().EndsWith("Roles/"))
                return Redirect("Index");
            if (HttpContext.Request.Path.ToString().EndsWith("Roles"))
                return Redirect("Roles/Index");

            ViewData["LiveRoles"] = _liveAccountManager.LiveRoles.ToArray();
            ViewData["LiveOperation"] = _liveAccountManager.LiveOperations.ToArray();
            ViewData["LiveRoleOperations"] = _liveAccountManager.LiveRoleOperations.ToArray();

            return Page();
        }

    }
}