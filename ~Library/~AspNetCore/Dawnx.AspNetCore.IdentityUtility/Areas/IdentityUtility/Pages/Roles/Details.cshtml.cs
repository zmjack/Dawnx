using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.IdentityUtility.Pages.Roles
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager
            = DIUtility.GetEntryService<RoleManager<IdentityRole>>();
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(ILogger<DetailsModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public IdentityRole Input { get; set; }

        public IActionResult OnGet()
        {
            if (!IdentityUtility.Advanced.IsUserAllowed(User))
                throw IdentityUtility.New_UnauthorizedAccessException;

            Input = _roleManager.FindByIdAsync(Request.Query["Id"]).Result;
            return Page();
        }

    }
}