using Dawnx.AspNetCore.LiveRegistry.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dawnx.AspNetCore.LiveRegistry
{
    public interface IAppRegistryDbContext
    {
        DbSet<AppRegistry> AppRegistries { get; set; }
    }
}
