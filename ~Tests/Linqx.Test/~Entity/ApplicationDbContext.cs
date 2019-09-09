using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace NLinq.Test
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base(new DbContextOptionsBuilder<ApplicationDbContext>().UseMySql("server=127.0.0.1;database=nlinqtest").Options)
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TrackModel> TrackModels { get; set; }
        public DbSet<EntityMonitorModel> EntityMonitorModels { get; set; }
        public DbSet<SimpleModel> SimpleModels { get; set; }
        public DbSet<CompositeKeyModel> CompositeKeyModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            NLinqUtility.ApplyAnnotations(this, modelBuilder, NLinqAnnotation.All);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            NLinqUtility.IntelliTrack(this, acceptAllChangesOnSuccess);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            NLinqUtility.IntelliTrack(this, acceptAllChangesOnSuccess);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }

}
