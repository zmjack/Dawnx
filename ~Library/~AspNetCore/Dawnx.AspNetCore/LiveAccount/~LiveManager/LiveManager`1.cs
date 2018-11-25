using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dawnx.AspNetCore.LiveAccount
{
    public partial class LiveManager<TDbContext> : LiveManager<TDbContext, IdentityUser>
        where TDbContext : IdentityDbContext<IdentityUser>, ILiveAccountDbContext
    {
    }
}
