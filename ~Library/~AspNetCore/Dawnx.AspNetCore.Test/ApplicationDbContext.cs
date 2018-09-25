using Dawnx.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dawnx.AspNetCore.Test
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("default").Options)
        {
        }

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

    public class SimpleModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [TrackCreationTime]
        public DateTime CreationTime { get; set; }

        [TrackLastWriteTime]
        public DateTime LastWriteTime { get; set; }

        [TrackTrim]
        public string ForTrim { get; set; }

        [TrackUpper]
        public string ForUpper { get; set; }

        [TrackLower]
        public string ForLower { get; set; }

        [TrackCondensed]
        public string ForCondensed { get; set; }

        [Track(typeof(RegexUtility), nameof(RegexUtility.IPRange) + "(" + nameof(ForTrim) + ")")]
        public string Automatic { get; set; }
    }
}
