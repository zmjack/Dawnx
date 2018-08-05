using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.IdentityUtility.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager
            = DIUtility.GetEntryService<UserManager<IdentityUser>>();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (!IdentityUtility.IsUserAllowed(User))
                throw IdentityUtility.New_UnauthorizedAccessException;

            if (HttpContext.Request.Path.ToString().EndsWith("Users/"))
                return Redirect("Index");
            if (HttpContext.Request.Path.ToString().EndsWith("Users"))
                return Redirect("Users/Index");

            ViewData["Items"] = _userManager.Users;
            return Page();
        }

    }
}