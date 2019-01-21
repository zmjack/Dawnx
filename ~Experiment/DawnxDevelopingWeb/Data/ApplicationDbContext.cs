using Dawnx.AspNetCore.AppSupport;
using Dawnx.AspNetCore.AppSupport.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DawnxDevelopingWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext, IAppRegistryDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppRegistry> AppRegistries { get; set; }
    }
}
