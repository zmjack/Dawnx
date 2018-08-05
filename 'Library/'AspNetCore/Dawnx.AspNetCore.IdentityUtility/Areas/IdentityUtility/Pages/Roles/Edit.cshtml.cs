using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.IdentityUtility.Pages.Roles
{
    public class EditModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager
            = DIUtility.GetEntryService<RoleManager<IdentityRole>>();
        private readonly ILogger<EditModel> _logger;

        public EditModel(ILogger<EditModel> logger)
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

        public IActionResult OnPost()
        {
            if (!IdentityUtility.Advanced.IsUserAllowed(User))
                throw IdentityUtility.New_UnauthorizedAccessException;

            if (ModelState.IsValid)
            {
                var role = _roleManager.FindByIdAsync(Input.Id).Result;
                var results = new[]
                {
                    _roleManager.SetRoleNameAsync(role, Input.Name).Result,
                    _roleManager.UpdateAsync(role).Result,
                };

                if (results.All(x => x.Succeeded))
                {
                    _logger.LogInformation($"Update role {role.Id}({role.Name}) succeeded.");
                    return Redirect("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty,
                        results.SelectMany(x => x.Errors).Select(x => x.Description).Join("<br/>"));
                    return Page();
                }
            }

            return Page();
        }

    }
}