﻿using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Actions
{
    [AllowAnonymous]
    public class ClearInvalidActionsModel : PageModel
    {
        private readonly ILiveManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveManager>(LiveManagerService.ServiceType);
        private readonly ILogger<ClearInvalidActionsModel> _logger;

        public ClearInvalidActionsModel(ILogger<ClearInvalidActionsModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPost()
        {
            if (!LiveAccountUtility.Authority?.Advanced?.IsUserAllowed(User) ?? false) throw Authority.New_UnauthorizedAccessException;

            _liveAccountManager.ClearInvalidActions();
            return Redirect("Index");
        }

    }
}