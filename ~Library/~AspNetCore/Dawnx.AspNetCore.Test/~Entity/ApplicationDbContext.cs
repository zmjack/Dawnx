using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Dawnx.AspNetCore.Test
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("default").Options)
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TrackModel> TrackModels { get; set; }
        public DbSet<EntityMonitorModel> EntityMonitorModels { get; set; }
        public DbSet<SimpleModel> SimpleModels { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.IntelliTrack(acceptAllChangesOnSuccess);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.IntelliTrack(acceptAllChangesOnSuccess);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }

}
