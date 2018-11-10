﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.IdentityUtility.Pages.Roles
{
    [AllowAnonymous]
    public class _GenerateConcurrencyStampModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager
            = DIUtility.GetEntryService<RoleManager<IdentityRole>>();
        private readonly ILogger<CreateModel> _logger;

        public _GenerateConcurrencyStampModel(ILogger<CreateModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Id { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!IdentityUtility.RoleControlPanel.IsUserAllowed(User))
                throw AuthorityUtility.New_UnauthorizedAccessException;

            var role = await _roleManager.FindByIdAsync(Input.Id);
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Generate ConcurrencyStamp for Role {role.Id}({role.Name}) succeeded.");
                return new JsonResult(new SimpleResponse
                {
                    state = "success",
                    model = role.ConcurrencyStamp,
                });
            }
            else
            {
                _logger.LogInformation($"Generate ConcurrencyStamp for Role {role.Id}({role.Name}) faild.");
                return new JsonResult(new SimpleResponse
                {
                    state = "faild",
                    status = "Invalid ConcurrencyStamp generation attempt.",
                    message = "Generate ConcurrencyStamp faild.",
                });
            }
        }

    }
}