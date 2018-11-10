using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Actions
{
    [AllowAnonymous]
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
            if (!LiveAccountUtility.NormalControlPanel.IsUserAllowed(User)) throw AuthorityUtility.New_UnauthorizedAccessException;

            _liveAccountManager.ClearInvalidActions();
            return Redirect("Index");
        }

    }
}