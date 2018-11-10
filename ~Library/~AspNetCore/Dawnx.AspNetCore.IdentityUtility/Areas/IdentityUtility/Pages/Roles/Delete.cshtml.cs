using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.IdentityUtility.Pages.Roles
{
    [AllowAnonymous]
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager
            = DIUtility.GetEntryService<RoleManager<IdentityRole>>();
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ILogger<DeleteModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public IdentityRole Input { get; set; }

        public IActionResult OnGet()
        {
            if (!IdentityUtility.RoleControlPanel.IsUserAllowed(User))
                throw AuthorityUtility.New_UnauthorizedAccessException;

            Input = _roleManager.FindByIdAsync(Request.Query["Id"]).Result;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!IdentityUtility.RoleControlPanel.IsUserAllowed(User))
                throw AuthorityUtility.New_UnauthorizedAccessException;

            if (ModelState.IsValid)
            {
                var role = _roleManager.FindByIdAsync(Input.Id).Result;
                var result = _roleManager.DeleteAsync(role).Result;
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Delete role {role.Id}({role.Name}) succeeded.");
                    return Redirect("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid deletion attempt.");
                    return Page();
                }
            }

            return Page();
        }

    }
}