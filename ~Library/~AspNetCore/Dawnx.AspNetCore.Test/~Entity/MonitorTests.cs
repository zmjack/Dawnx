using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit;
using Dawnx.AspNetCore;
using System.Threading;
using System.Threading.Tasks;
using Dawnx.Utilities;
using System.Linq;
using System.Collections.Generic;
using Dawnx.AspNetCore.Entity;

namespace Dawnx.AspNetCore.Test
{
    public class MonitorTests
    {
        [Fact]
        public void Test1()
        {
            var log = new List<string>();

            EntityMonitor.RegisterMonitor<SimpleModel>(EntityState.Added, new MonitorInvoker((user, properties) =>
            {
                log.Add($"{user}\t{nameof(EntityState.Added)}");
            }));

            EntityMonitor.RegisterMonitor<SimpleModel>(EntityState.Modified, new MonitorInvoker((user, properties) =>
            {
                log.Add($"{user}\t{nameof(EntityState.Modified)}");
            }));

            EntityMonitor.RegisterMonitor<SimpleModel>(EntityState.Deleted, new MonitorInvoker((user, properties) =>
            {
                log.Add($"{user}\t{nameof(EntityState.Deleted)}");
            }));

            using (var context = new ApplicationDbContext())
            {
                context.Add(new SimpleModel
                {
                    ProductName = "A",
                });
                context.SaveChanges();
                Assert.Empty(log);

                // Added
                var item = new SimpleModel
                {
                    MonitorExecutor = "u1",
                    ProductName = "b",
                };
                context.Add(item);
                context.SaveChanges();
                Assert.Equal($"{item.MonitorExecutor}\t{nameof(EntityState.Added)}", log.Last());

                // Modified
                var result = context.SimpleModels.First();
                result.MonitorExecutor = "u1";
                result.ProductName = "B";
                context.SaveChanges();
                Assert.Equal($"{result.MonitorExecutor}\t{nameof(EntityState.Modified)}", log.Last());

                // Deleted
                context.RemoveRange(context.SimpleModels.Each(_ => _.MonitorExecutor = "u2"));
                context.SaveChanges();
                Assert.Equal(new[]
                {
                    $"{result.MonitorExecutor}\t{nameof(EntityState.Deleted)}",
                    $"{result.MonitorExecutor}\t{nameof(EntityState.Deleted)}",
                }, log.TakeLast(2));
            }

        }

    }
}
