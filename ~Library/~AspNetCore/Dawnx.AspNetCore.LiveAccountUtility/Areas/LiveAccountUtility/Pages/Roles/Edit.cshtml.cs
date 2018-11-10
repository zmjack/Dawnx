using System;
using System.Linq;
using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Roles
{
    [AllowAnonymous]
    public class EditModel : PageModel
    {
        private readonly ILiveAccountManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveAccountManager>(LiveAccountDependencyInjection.LiveAccountService);
        private readonly ILogger<EditModel> _logger;

        public EditModel(ILogger<EditModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public LiveRole Input { get; set; }

        public IActionResult OnGet()
        {
            if (!LiveAccountUtility.Authority?.Advanced?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            Input = _liveAccountManager.LiveRoles.Find(Guid.Parse(Request.Query["Id"]));

            ViewData["LiveOperations"] = _liveAccountManager.LiveOperations.ToArray();
            ViewData["RoleLiveOperations"] = _liveAccountManager.GetRoleOperations(Input.Id);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!LiveAccountUtility.Authority?.Advanced?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            Input = _liveAccountManager.LiveRoles.Find(Guid.Parse(Request.Query["Id"]));

            ViewData["LiveOperations"] = _liveAccountManager.LiveOperations.ToArray();
            ViewData["RoleLiveOperations"] = _liveAccountManager.GetRoleOperations(Input.Id);

            if (ModelState.IsValid)
            {
                using (_liveAccountManager.FastProcessing)
                {
                    _liveAccountManager.UpdateLiveRole(Input);
                    _liveAccountManager.SetRoleOperations(Input.Id,
                        Request.Form["LiveOperations"].Select(x => Guid.Parse(x)).ToArray());
                }
                return Redirect("Index");
            }

            return Page();
        }

    }
}