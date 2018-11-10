using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.IdentityUtility.Pages.Users
{
    [AllowAnonymous]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager
            = DIUtility.GetEntryService<UserManager<IdentityUser>>();
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ILogger<DeleteModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public IdentityUser Input { get; set; }

        public IActionResult OnGet()
        {
            if (!IdentityUtility.UserControlPanel.IsUserAllowed(User))
                throw AuthorityUtility.New_UnauthorizedAccessException;

            Input = _userManager.FindByIdAsync(Request.Query["Id"]).Result;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!IdentityUtility.UserControlPanel.IsUserAllowed(User))
                throw AuthorityUtility.New_UnauthorizedAccessException;

            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(Input.Id).Result;
                var result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Delete role {user.Id}({user.UserName}) succeeded.");
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