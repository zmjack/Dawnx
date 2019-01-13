using Dawnx.AspNetCore.AppSupport.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dawnx.AspNetCore.AppSupport
{
    public interface IAppRegistryDbContext
    {
        DbSet<AppRegistry> AppRegistries { get; set; }
    }
}
