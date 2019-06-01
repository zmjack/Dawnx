using Dawnx;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Dawnx.AspNetCore.IdentityUtility.Pages.Users
{
    [AllowAnonymous]
    public class EditModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager
            = DIUtility.GetEntryService<RoleManager<IdentityRole>>();
        private readonly UserManager<IdentityUser> _userManager
            = DIUtility.GetEntryService<UserManager<IdentityUser>>();
        private readonly ILogger<EditModel> _logger;

        public EditModel(ILogger<EditModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public IdentityUser Input { get; set; }

        public IActionResult OnGet()
        {
            if (!IdentityUtility.Authority?.UserManager?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            Input = _userManager.FindByIdAsync(Request.Query["Id"]).Result;

            var roles = _roleManager.Roles.Select(x => x.Name).ToArray();
            var userRoles = _userManager.GetRolesAsync(Input).Result.ToArray();
            ViewData["Roles"] = roles;
            ViewData["UserRoles"] = userRoles;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!IdentityUtility.Authority?.UserManager?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            var roles = _roleManager.Roles.Select(x => x.Name).ToArray();
            var userRoles = _userManager.GetRolesAsync(Input).Result.ToArray();
            ViewData["Roles"] = roles;
            ViewData["UserRoles"] = userRoles;

            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(Input.Id).Result;
                user.Self(_ =>
                {
                    _.PhoneNumberConfirmed = Input.PhoneNumberConfirmed;
                    _.EmailConfirmed = Input.EmailConfirmed;
                    _.AccessFailedCount = Input.AccessFailedCount;
                });
                var results = new[]
                {
                    _userManager.SetUserNameAsync(user, Input.UserName).Result,
                    _userManager.SetEmailAsync(user, Input.Email).Result,
                    _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber).Result,
                    _userManager.SetLockoutEnabledAsync(user, Input.LockoutEnabled).Result,
                    Input.LockoutEnabled.For(_ =>
                    {
                        if (_)
                            return _userManager.SetLockoutEndDateAsync(user, Input.LockoutEnd).Result;
                        else return IdentityResult.Success;
                    }),
                    _userManager.UpdateAsync(user).Result,
                    Request.Form["password"].ToString().For(password =>
                    {
                        var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                        if (!password.IsNullOrEmpty())
                            return _userManager.ResetPasswordAsync(user, token, password).Result;
                        else return IdentityResult.Success;
                    }),
                    _userManager.RemoveFromRolesAsync(user, userRoles).Result,
                    _userManager.AddToRolesAsync(user, Request.Form["roles"]).Result,
                };

                if (results.All(x => x.Succeeded))
                {
                    _logger.LogInformation($"Update role {user.Id}({user.UserName}) succeeded.");
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