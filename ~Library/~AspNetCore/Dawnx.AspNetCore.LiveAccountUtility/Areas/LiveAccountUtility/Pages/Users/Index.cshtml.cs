using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Users
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
            if (!LiveAccountUtility.Authority?.User?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            if (HttpContext.Request.Path.ToString().EndsWith("Users/"))
                return Redirect("Index");
            if (HttpContext.Request.Path.ToString().EndsWith("Users"))
                return Redirect("Users/Index");

            ViewData["Users"] = _liveAccountManager.Users.OrderBy(x => x.UserName).ToArray();
            ViewData["LiveRoles"] = _liveAccountManager.LiveRoles.OrderBy(x => x.Name).ToArray();
            ViewData["LiveUserRoles"] = _liveAccountManager.LiveUserRoles.ToArray();

            return Page();
        }

    }
}