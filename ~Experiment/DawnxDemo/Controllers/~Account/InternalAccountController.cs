using Dawnx.AspNetCore.Authorization;
using Dawnx.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace DawnxDemo.Controllers
{
    public class InternalAccountController : Controller
    {
        public IActionResult SignIn(string ticket, string returnUrl)
        {
            var tticket = TemporaryTicket.Parse(Program.RemoteAuthSecurity, ticket);
            if (tticket.IsValid)
            {
                var data = JsonConvert.DeserializeObject<JToken>(tticket.Data);
                var name = data["name"].Value<string>();
                var roles = data["roles"].Value<string[]>();
                HttpContext.SignInAsync("Internal", new ClaimsPrincipal(new SimpleClaimsIdentity(name, roles)));
                return Redirect(returnUrl);
            }
            else return Content("Ticket is invalid.");
        }

        public ContentResult Denied() => Content("Denied.");

    }
}