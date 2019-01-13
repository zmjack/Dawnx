using System.Linq;
using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Users
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly ILiveManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveManager>(LiveManagerService.ServiceType);
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(ILogger<DetailsModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public IdentityUser<string> Input { get; set; }

        public IActionResult OnGet()
        {
            if (!LiveAccountUtility.Authority?.User?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            var user = ((dynamic)_liveAccountManager).Users.Find(Request.Query["Id"]);
            Input = user as IdentityUser<string>;

            ViewData["LiveRoles"] = _liveAccountManager.LiveRoles.ToArray();
            ViewData["UserLiveRoles"] = _liveAccountManager.GetUserRoles(Input.UserName);

            return Page();
        }

    }
}