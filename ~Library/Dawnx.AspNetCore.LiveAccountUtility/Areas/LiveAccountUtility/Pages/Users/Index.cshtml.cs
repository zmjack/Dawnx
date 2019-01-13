using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Users
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILiveManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveManager>(LiveManagerService.ServiceType);
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

            var users =
                Enumerable.ToArray(
                    Enumerable.Cast<IdentityUser<string>>(
                        ((dynamic)_liveAccountManager).Users));
            ViewData["Users"] = users;
            ViewData["LiveRoles"] = _liveAccountManager.LiveRoles.OrderBy(x => x.Name).ToArray();
            ViewData["LiveUserRoles"] = _liveAccountManager.LiveUserRoles.ToArray();

            return Page();
        }

    }
}