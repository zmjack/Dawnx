using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Operations
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
        public LiveOperation Input { get; set; }

        public IActionResult OnGet()
        {
            if (!LiveAccountUtility.RoleAndOperationControlPanel.IsUserAllowed(User))
                throw AuthorityUtility.New_UnauthorizedAccessException;

            Input = _liveAccountManager.LiveOperations.Find(Request.Query["Id"]);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!LiveAccountUtility.RoleAndOperationControlPanel.IsUserAllowed(User))
                throw AuthorityUtility.New_UnauthorizedAccessException;

            if (ModelState.IsValid)
            {
                _liveAccountManager.DeleteOperation(Input);
                return Redirect("Index");
            }

            return Page();
        }

    }
}