using Dawnx.AspNetCore.LiveAccount;
using Dawnx.AspNetCore.LiveAccount.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Roles
{
    [AllowAnonymous]
    public class CreateModel : PageModel
    {
        private readonly ILiveManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveManager>(LiveDependencyInjection.LiveAccountService);
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ILogger<CreateModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public LiveRole Input { get; set; }

        public IActionResult OnGet()
        {
            if (!LiveAccountUtility.Authority?.Advanced?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            ViewData["LiveOperations"] = _liveAccountManager.LiveOperations.ToArray();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!LiveAccountUtility.Authority?.Advanced?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            ViewData["LiveOperations"] = _liveAccountManager.LiveRoleOperations.ToArray();

            if (ModelState.IsValid)
            {
                using (_liveAccountManager.FastProcessing)
                {
                    _liveAccountManager.CreateLiveRole(Input);
                    _liveAccountManager.SetRoleOperations(Input.Id,
                        Request.Form["LiveOperations"].Select(x => Guid.Parse(x)).ToArray());
                }
                return Redirect("Index");
            }

            return Page();
        }

    }
}