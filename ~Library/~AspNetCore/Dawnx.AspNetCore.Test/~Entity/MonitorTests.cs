using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Dawnx.Entity;

namespace Dawnx.AspNetCore.Test
{
    public class MonitorTests
    {
        [Fact]
        public void Test1()
        {
            var log = new List<string>();

            EntityMonitor.Register<SimpleModel>(param =>
            {
                switch (param.State)
                {
                    case EntityState.Added:
                        log.Add($"{param.Carry}\t{nameof(EntityState.Added)}");
                        break;
                    case EntityState.Modified:
                        log.Add($"{param.Carry}\t{nameof(EntityState.Modified)}");
                        break;
                    case EntityState.Deleted:
                        log.Add($"{param.Carry}\t{nameof(EntityState.Deleted)}");
                        break;
                }
            });

            using (var context = new ApplicationDbContext())
            {
                context.Add(new SimpleModel
                {
                    ProductName = "A",
                });
                context.SaveChanges();
                Assert.Equal($"\t{nameof(EntityState.Added)}", log.Last());

                // Added
                var item = new SimpleModel
                {
                    ProductName = "b",
                }.MonitorCarry("u1");
                context.Add(item);
                context.SaveChanges();
                Assert.Equal($"u1\t{nameof(EntityState.Added)}", log.Last());

                // Modified
                var result = context.SimpleModels.First();
                result.ProductName = "B";
                result.MonitorCarry("u2");
                context.SaveChanges();
                Assert.Equal($"u2\t{nameof(EntityState.Modified)}", log.Last());

                // Deleted
                context.SimpleModels.AsEnumerable().MonitorCarry("u3");
                context.RemoveRange(context.SimpleModels);
                context.SaveChanges();
                Assert.Equal(new[]
                {
                    $"{result.MonitorCarry as string}\t{nameof(EntityState.Deleted)}",
                    $"{result.MonitorCarry as string}\t{nameof(EntityState.Deleted)}",
                }, log.TakeLast(2));
            }

        }

    }
}