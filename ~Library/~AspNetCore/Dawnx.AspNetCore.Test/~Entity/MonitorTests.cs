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

            EntityMonitor.RegisterForAdded(new MonitorInvoker<SimpleModel>((model, carry, properties) =>
            {
                log.Add($"{carry}\t{nameof(EntityState.Added)}");
            }));

            EntityMonitor.RegisterForModified(new MonitorInvoker<SimpleModel>((model, carry, properties) =>
            {
                log.Add($"{carry}\t{nameof(EntityState.Modified)}");
            }));

            EntityMonitor.RegisterForDeleted(new MonitorInvoker<SimpleModel>((model, carry, properties) =>
            {
                log.Add($"{carry}\t{nameof(EntityState.Deleted)}");
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
                    ProductName = "b",
                }.EnableMonitor("u1");
                context.Add(item);
                context.SaveChanges();
                Assert.Equal($"u1\t{nameof(EntityState.Added)}", log.Last());

                // Modified
                var result = context.SimpleModels.First();
                result.ProductName = "B";
                result.EnableMonitor("u2");
                context.SaveChanges();
                Assert.Equal($"u2\t{nameof(EntityState.Modified)}", log.Last());

                // Deleted
                context.SimpleModels.AsEnumerable().EnableMonitor("u3");
                context.RemoveRange(context.SimpleModels);
                context.SaveChanges();
                Assert.Equal(new[]
                {
                    $"{result.MonitorState as string}\t{nameof(EntityState.Deleted)}",
                    $"{result.MonitorState as string}\t{nameof(EntityState.Deleted)}",
                }, log.TakeLast(2));
            }

        }

    }
}
