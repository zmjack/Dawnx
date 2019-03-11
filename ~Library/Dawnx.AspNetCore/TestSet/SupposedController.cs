using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dawnx.AspNetCore.TestSet
{
    public class SupposedController<TController>
        where TController : Controller
    {
        public TController Controller { get; }

        private SupposedController(TController controller)
        {
            Controller = controller;
        }

        public static SupposedController<TController> Create(TController controller, string user, string role)
        {
            var ret = new SupposedController<TController>(controller);
            ret.Controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity().Self(_ =>
                    {
                        _.AddClaims(new[]
                        {
                                new Claim(ClaimTypes.Name, user),
                                new Claim(ClaimTypes.Role, role),
                            });
                    })),
                }
            };
            return ret;
        }

    }
}
