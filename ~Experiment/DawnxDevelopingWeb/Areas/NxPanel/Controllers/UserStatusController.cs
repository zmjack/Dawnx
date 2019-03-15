using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dawnx;

namespace NxPanel.Controllers
{
    [Area("NxPanel")]
    public class UserStatusController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.TopActive = "/Application";
            ViewBag.SidebarActive = "/Identity";

            return View();
        }

        [HttpGet]
        public JsonResult UserIdentities()
        {
            var data = User.Identities.Select((identity, key) => new
            {
                key,
                identity.AuthenticationType,
                identity.Name,
                Roles = identity.Claims
                     .Where(x => x.Type == identity.RoleClaimType)
                     .Select(x => x.Value).Join(",").ToArray(),
                Claims = identity.Claims
                     .Select(x => new { x.Type, x.Value }).ToArray(),
            }).ToArray();

            return Json(data);
        }
    }
}