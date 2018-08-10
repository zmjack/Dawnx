using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Actions
{
    [AllowAnonymous]
    public class SyncActionsModel : PageModel
    {
        private readonly ILiveAccountManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveAccountManager>(LiveAccountDependencyInjection.LiveAccountService);
        private readonly ILogger<SyncActionsModel> _logger;

        public SyncActionsModel(ILogger<SyncActionsModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPost()
        {
            if (!LiveAccountUtility.IsUserAllowed(User)) throw LiveAccountUtility.New_UnauthorizedAccessException;

            _liveAccountManager.SyncActions();
            return Redirect("Index");
        }

    }
}