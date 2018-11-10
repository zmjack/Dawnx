using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Roles
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly ILiveAccountManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveAccountManager>(LiveAccountDependencyInjection.LiveAccountService);
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(ILogger<DetailsModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public LiveRole Input { get; set; }

        public IActionResult OnGet()
        {
            if (!LiveAccountUtility.RoleAndOperationControlPanel.IsUserAllowed(User))
                throw AuthorityUtility.New_UnauthorizedAccessException;

            Input = _liveAccountManager.LiveRoles.Find(Guid.Parse(Request.Query["Id"]));

            ViewData["LiveOperations"] = _liveAccountManager.LiveRoleOperations.ToArray();
            ViewData["RoleLiveOperations"] = _liveAccountManager.GetRoleOperations(Input.Id);
            
            return Page();
        }

    }
}