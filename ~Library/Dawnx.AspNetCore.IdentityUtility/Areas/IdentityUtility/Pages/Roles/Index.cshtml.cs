using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.IdentityUtility.Pages.Roles
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager
            = DIUtility.GetEntryService<RoleManager<IdentityRole>>();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (!IdentityUtility.Authority?.Role?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            if (HttpContext.Request.Path.ToString().EndsWith("Roles/"))
                return Redirect("Index");
            if (HttpContext.Request.Path.ToString().EndsWith("Roles"))
                return Redirect("Roles/Index");

            ViewData["Items"] = _roleManager.Roles;
            return Page();
        }

    }
}