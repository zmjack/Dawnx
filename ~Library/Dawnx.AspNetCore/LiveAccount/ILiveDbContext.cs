using Dawnx.AspNetCore.LiveAccount.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dawnx.AspNetCore.LiveAccount
{
    public interface ILiveAccountDbContext
    {
        DbSet<LiveRole> LiveRoles { get; set; }
        DbSet<LiveUserRole> LiveUserRoles { get; set; }
        DbSet<LiveRoleOperation> LiveRoleOperations { get; set; }
        DbSet<LiveOperation> LiveOperations { get; set; }
        DbSet<LiveOperationAction> LiveOperationActions { get; set; }
        DbSet<LiveAction> LiveActions { get; set; }
    }
}
