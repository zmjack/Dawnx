using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dawnx.AspNetCore.LiveAccount
{
    public partial class LiveAccountManager<TDbContext>
        where TDbContext : IdentityDbContext, ILiveAccountDbContext
    {
        private readonly TDbContext _context;

        public LiveAccountManager()
        {
            _context = DIUtility.GetEntryService<TDbContext>();
        }

        public LiveAccountTransaction FastProcessing => new LiveAccountTransaction(this);

        public bool CheckAuthorization(ActionExecutingContext actionExecutingContext)
        {
            var descriptor = actionExecutingContext.ActionDescriptor as ControllerActionDescriptor;
            var areaName = descriptor.RouteValues["area"];
            var controllerName = descriptor.ControllerName;
            var actionName = descriptor.ActionName;
            var username = actionExecutingContext.HttpContext.User.Identity.Name;

            var user = Users.SingleOrDefault(x => x.UserName == username);
            if (user is null) return false;

            var allowedActions = LiveUserRoles
                .Include(x => x.RoleLink.RoleOperations)
                .Where(x => x.User == user.Id)
                .SelectMany(x => x.RoleLink.RoleOperations)
                .SelectMany(x => x.OperationLink.OperationActions)
                .Select(x => x.ActionLink)
                .Where(x => x.IsExisted)
                .ToArray();

            return allowedActions.Any(x =>
                (x.Area is null && x.Controller is null && x.Action is null)
                || (x.Area == areaName && x.Controller is null && x.Action is null)
                || (x.Area == areaName && x.Controller == controllerName && x.Action is null)
                || (x.Area == areaName && x.Controller == controllerName && x.Action == actionName));
        }

    }
}
