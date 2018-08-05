﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dawnx.AspNetCore.IdentityUtility.Pages.Users
{
    public class _GenerateConcurrencyStampModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager
            = DIUtility.GetEntryService<UserManager<IdentityUser>>();
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

        public IActionResult OnPost()
        {
            if (!IdentityUtility.IsUserAllowed(User))
                throw IdentityUtility.New_UnauthorizedAccessException;

            var user = _userManager.FindByIdAsync(Input.Id).Result;
            var result = _userManager.UpdateAsync(user).Result;
            if (result.Succeeded)
            {
                _logger.LogInformation($"Generate ConcurrencyStamp for Role {user.Id}({user.UserName}) succeeded.");
                return new JsonResult(new SimpleResponse
                {
                    state = "success",
                    model = user.ConcurrencyStamp,
                });
            }
            else
            {
                _logger.LogInformation($"Generate ConcurrencyStamp for Role {user.Id}({user.UserName}) faild.");
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