using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit;
using Dawnx.AspNetCore;
using System.Threading;
using System.Threading.Tasks;
using Dawnx.Utilities;

namespace Dawnx.Test
{
    public class TrackTests
    {
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

        [Fact]
        public void Test1()
        {
            using (var context = new ApplicationDbContext())
            {
                var model = new SimpleModel
                {
                    ForTrim = "   127.0.0.* ",
                    ForLower = "Dawnx",
                    ForUpper = "Dawnx",
                    ForCondensed = "  Welcome to   Dawnx  ",
                };
                context.SimpleModels.Add(model);
                context.SaveChanges();

                Assert.Equal("127.0.0.*", model.ForTrim);
                Assert.Equal("dawnx", model.ForLower);
                Assert.Equal("DAWNX", model.ForUpper);
                Assert.Equal("Welcome to Dawnx", model.ForCondensed);
                Assert.Equal(@"127\.0\.0\.(?:[1-2]\d(?<!2[6-9])\d(?<!25[6-9])|\d\d|[0-9])", model.Automatic);
            }

        }

    }
}
