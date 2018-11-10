using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.IdentityUtility.Pages.Users
{
    [AllowAnonymous]
    public class CreateModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager
            = DIUtility.GetEntryService<RoleManager<IdentityRole>>();
        private readonly UserManager<IdentityUser> _userManager
            = DIUtility.GetEntryService<UserManager<IdentityUser>>();
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ILogger<CreateModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public IdentityUser Input { get; set; }

        public IActionResult OnGet()
        {
            if (!IdentityUtility.UserControlPanel.IsUserAllowed(User))
                throw AuthorityUtility.New_UnauthorizedAccessException;

            Input = new IdentityUser { LockoutEnd = DateTime.Now };

            var roles = _roleManager.Roles.Select(x => x.Name).ToArray();
            ViewData["Roles"] = roles;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!IdentityUtility.UserControlPanel.IsUserAllowed(User))
                throw AuthorityUtility.New_UnauthorizedAccessException;

            var roles = _roleManager.Roles.Select(x => x.Name).ToArray();
            ViewData["Roles"] = roles;

            var password = Request.Form["password"].ToString();
            if (password.IsNullOrEmpty())
            {
                ModelState.AddModelError("password", "Password can not be null.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityUser(Input.UserName);
                var userResult = _userManager.CreateAsync(user, password).Result;

                if (userResult.Succeeded)
                {
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
                        _userManager.SetLockoutEndDateAsync(user, Input.LockoutEnd).Result,
                        Input.LockoutEnabled.For(_ =>
                        {
                            if (_)
                                return _userManager.SetLockoutEndDateAsync(user, Input.LockoutEnd).Result;
                            else return IdentityResult.Success;
                        }),
                        _userManager.UpdateAsync(user).Result,
                        _userManager.AddToRolesAsync(user, Request.Form["roles"]).Result,
                    };

                    if (results.All(x => x.Succeeded))
                    {
                        _logger.LogInformation($"Create role {user.Id}({user.UserName}) succeeded.");
                        return Redirect("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                            results.SelectMany(x => x.Errors).Select(x => x.Description).Join("<br/>"));
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, userResult.Errors.Select(x => x.Description).Join("<br/>"));
                    return Page();
                }
            }

            return Page();
        }

    }
}