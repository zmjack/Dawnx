﻿using Dawnx.AspNetCore.LiveAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Dawnx.AspNetCore.LiveAccountUtility.Pages.Users
{
    [AllowAnonymous]
    public class EditModel : PageModel
    {
        private readonly ILiveManager _liveAccountManager
            = DIUtility.GetEntryService<ILiveManager>(LiveManagerService.ServiceType);
        private readonly ILogger<EditModel> _logger;

        public EditModel(ILogger<EditModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public IdentityUser<string> Input { get; set; }

        public IActionResult OnGet()
        {
            if (!LiveAccountUtility.Authority?.User?.IsUserAllowed(User) ?? false)
                throw Authority.New_UnauthorizedAccessException;

            var user = ((dynamic)_liveAccountManager).Users.Find(Request.Query["Id"]);
            Input = user as IdentityUser<string>;

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