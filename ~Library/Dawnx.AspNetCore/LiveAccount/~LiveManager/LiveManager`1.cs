using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dawnx.AspNetCore.LiveAccount
{
    public partial class LiveManager<TDbContext> : LiveManager<TDbContext, IdentityUser>
        where TDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>, ILiveAccountDbContext
    {
    }
}
