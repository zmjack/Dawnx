using System;
using System.Linq;
using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Users
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
        public IdentityUser Input { get; set; }

        public IActionResult OnGet()
        {
            if (!LiveAccountUtility.IsUserAllowed(User))
                throw LiveAccountUtility.New_UnauthorizedAccessException;

            Input = _liveAccountManager.Users.Find(Guid.Parse(Request.Query["Id"]));

            ViewData["LiveRoles"] = _liveAccountManager.LiveRoles.ToArray();
            ViewData["UserLiveRoles"] = _liveAccountManager.GetUserRoles(Input.UserName);

            return Page();
        }

    }
}