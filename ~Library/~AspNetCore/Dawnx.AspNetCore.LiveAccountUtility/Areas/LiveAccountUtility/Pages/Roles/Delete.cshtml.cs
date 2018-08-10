using System;
using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Roles
{
    [AllowAnonymous]
    public class DeleteModel : PageModel
    {
        private readonly ILiveAccountManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveAccountManager>(LiveAccountDependencyInjection.LiveAccountService);
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ILogger<DeleteModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public LiveRole Input { get; set; }

        public IActionResult OnGet()
        {
            if (!LiveAccountUtility.Advanced.IsUserAllowed(User))
                throw LiveAccountUtility.New_UnauthorizedAccessException;

            Input = _liveAccountManager.LiveRoles.Find(Guid.Parse(Request.Query["Id"]));
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!LiveAccountUtility.Advanced.IsUserAllowed(User))
                throw LiveAccountUtility.New_UnauthorizedAccessException;

            if (ModelState.IsValid)
            {
                _liveAccountManager.DeleteLiveRole(Input);
                return Redirect("Index");
            }

            return Page();
        }

    }
}