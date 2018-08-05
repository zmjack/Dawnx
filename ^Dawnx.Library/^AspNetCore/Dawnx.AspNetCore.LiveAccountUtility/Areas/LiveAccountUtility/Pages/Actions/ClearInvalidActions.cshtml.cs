using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Actions
{
    public class ClearInvalidActionsModel : PageModel
    {
        private readonly ILiveAccountManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveAccountManager>(LiveAccountDependencyInjection.LiveAccountService);
        private readonly ILogger<ClearInvalidActionsModel> _logger;

        public ClearInvalidActionsModel(ILogger<ClearInvalidActionsModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPost()
        {
            if (!LiveAccountUtility.IsUserAllowed(User)) throw LiveAccountUtility.New_UnauthorizedAccessException;

            _liveAccountManager.ClearInvalidActions();
            return Redirect("Index");
        }

    }
}