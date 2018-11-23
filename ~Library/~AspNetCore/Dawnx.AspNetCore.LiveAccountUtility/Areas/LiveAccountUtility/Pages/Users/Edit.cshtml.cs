using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Dawnx;
using System.Linq;
using Dawnx.AspNetCore.LiveAccount;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Users
{
    [AllowAnonymous]
    public class EditModel : PageModel
    {
        private readonly ILiveManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveManager>(LiveDependencyInjection.LiveAccountService);
        private readonly ILogger<EditModel> _logger;

        public EditModel(ILogger<EditModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public IdentityUser Input { get; set; }

        public IActionResult OnGet()
        {
            if (!LiveAccountUtility.Authority?.User?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            Input = _liveAccountManager.Users.Find(Request.Query["Id"]);

            ViewData["LiveRoles"] = _liveAccountManager.LiveRoles
                .Include(x => x.RoleOperations).ThenInclude(x => x.OperationLink)
                .ToArray();
            ViewData["UserLiveRoles"] = _liveAccountManager.GetUserRoles(Input.UserName);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!LiveAccountUtility.Authority?.User?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            ViewData["LiveRoles"] = _liveAccountManager.LiveRoles
                .Include(x => x.RoleOperations).ThenInclude(x => x.OperationLink)
                .ToArray();
            ViewData["UserLiveRoles"] = _liveAccountManager.GetUserRoles(Input.UserName);

            if (ModelState.IsValid)
            {
                using (_liveAccountManager.FastProcessing)
                {
                    _liveAccountManager.SetUserRoles(Input.UserName,
                        Request.Form["LiveRoles"].Select(x => Guid.Parse(x)).ToArray());
                }
                return Redirect("Index");
            }

            return Page();
        }

    }
}