using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.IdentityUtility.Pages.Roles
{
    [AllowAnonymous]
    public class CreateModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager
            = DIUtility.GetEntryService<RoleManager<IdentityRole>>();
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ILogger<CreateModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public IdentityRole Input { get; set; }

        public IActionResult OnGet()
        {
            if (!IdentityUtility.Authority?.RoleManager?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!IdentityUtility.Authority?.RoleManager?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            if (ModelState.IsValid)
            {
                var role = new IdentityRole(Input.Name);
                var result = _roleManager.CreateAsync(role).Result;
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Create role {role.Id}({role.Name}) succeeded.");
                    return Redirect("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid creation attempt.");
                    return Page();
                }
            }

            return Page();
        }

    }
}