using Dawnx;
using Dawnx.Entity;
using Dawnx.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Xunit;

namespace Dawnx.Test
{
    public class WhereStrategyTests
    {
        [Fact]
        public void Test1()
        {
            using (var context = new ApplicationDbContext())
            {
                context.CompLevels.AddRange(new CompLevel[]
                {
                    new CompLevel { Name = "Broze1", Level = 11 },
                    new CompLevel { Name = "Broze2", Level = 12 },
                    new CompLevel { Name = "Silver1", Level = 21 },
                    new CompLevel { Name = "Silver2", Level = 22 },
                    new CompLevel
                    {
                        Name = "Gold1",
                        Level = 31,
                        People = new[]
                        {
                            new Person { Name = "Lux", Level = 30 },
                        },
                    },
                    new CompLevel
                    {
                        Name = "Gold2",
                        Level = 32,
                        People = new []
                        {
                            new Person { Name = "Fizz", Level = 2 },
                        },
                    },
                    new CompLevel
                    {
                        Name = "Platinum1",
                        Level = 40,
                        People = new []
                        {
                            new Person { Name = "Ashe", Level = 30 },
                            new Person { Name = "Anivia", Level = 28 },
                        },
                    },
                });
                context.SaveChanges();

                new WhereSearchStrategy<CompLevel>("Ashe", x => new { Names = x.People.Select(p => p.Name) }).Self(_ =>
                    Assert.Equal(new[] { "Platinum1" }, context.CompLevels.WhereStrategy(_).Select(x => x.Name).ToArray()));
                new WhereMatchStrategy<CompLevel>("30", x => new { levels = x.People.Select(p => p.Level) }).Self(_ =>
                    Assert.Equal(new[] { "Gold1", "Platinum1" }, context.CompLevels.WhereStrategy(_).Select(x => x.Name).ToArray()));
                new WhereSearchStrategy<Person>("at", x => new { x.CompLevelLink.Name }).Self(_ =>
                    Assert.Equal(new[] { "Ashe", "Anivia" }, context.People.WhereStrategy(_).Select(x => x.Name).ToArray()));

                //MatchStrategy
                TestCheck(new WhereMatchStrategy<CompLevel>("22", x => x.Name), context.CompLevels,
                    0, "x => x.Name.Equals(\"22\")");
                TestCheck(new WhereMatchStrategy<CompLevel>("22", x => x.Level), context.CompLevels,
                    1, "x => Convert(x.Level, Object).ToString().Equals(\"22\")");
                TestCheck(new WhereMatchStrategy<CompLevel>("22", x => x.Level.ToString()), context.CompLevels,
                    1, "x => x.Level.ToString().Equals(\"22\")");
                TestCheck(new WhereMatchStrategy<CompLevel>("22", x => new { x.Name }), context.CompLevels,
                    0, "x => x.Name.Equals(\"22\")");
                TestCheck(new WhereMatchStrategy<CompLevel>("22", x => new { x.Name, x.Level }), context.CompLevels,
                    1, "x => (x.Name.Equals(\"22\") OrElse x.Level.ToString().Equals(\"22\"))");
                TestCheck(new WhereMatchStrategy<CompLevel>("22", x => new { x.Name, Level = x.Level.ToString() }), context.CompLevels,
                    1, "x => (x.Name.Equals(\"22\") OrElse x.Level.ToString().Equals(\"22\"))");

                //SearchStrategy
                TestCheck(new WhereSearchStrategy<CompLevel>("1", x => x.Name), context.CompLevels,
                    4, "x => x.Name.Contains(\"1\")");
                TestCheck(new WhereSearchStrategy<CompLevel>("1", x => x.Level), context.CompLevels,
                    4, "x => Convert(x.Level, Object).ToString().Contains(\"1\")");
                TestCheck(new WhereSearchStrategy<CompLevel>("1", x => x.Level.ToString()), context.CompLevels,
                    4, "x => x.Level.ToString().Contains(\"1\")");
                TestCheck(new WhereSearchStrategy<CompLevel>("1", x => new { x.Name }), context.CompLevels,
                    4, "x => x.Name.Contains(\"1\")");
                TestCheck(new WhereSearchStrategy<CompLevel>("1", x => new { x.Name, x.Level }), context.CompLevels,
                    5, "x => (x.Name.Contains(\"1\") OrElse x.Level.ToString().Contains(\"1\"))");
                TestCheck(new WhereSearchStrategy<CompLevel>("1", x => new { x.Name, Level = x.Level.ToString() }), context.CompLevels,
                    5, "x => (x.Name.Contains(\"1\") OrElse x.Level.ToString().Contains(\"1\"))");

            }
        }

        private void TestCheck<T>(WhereStringStrategy<T> strategy, DbSet<T> dbSet, int count, string strategyExpression)
            where T : class
        {
            Assert.Equal(strategyExpression, strategy.StrategyExpression.ToString());
            Assert.Equal(count, dbSet.WhereStrategy(strategy).Count());
        }

        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext()
                : base(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("default").Options)
            {
            }

            public DbSet<CompLevel> CompLevels { get; set; }
            public DbSet<Person> People { get; set; }
        }

        public class CompLevel
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }

            public string Name { get; set; }

            public int Level { get; set; }

            public virtual ICollection<Person> People { get; set; }
        }

        public class Person
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }

            [ForeignKey(nameof(CompLevelLink))]
            public Guid CompLevel { get; set; }

            public string Name { get; set; }

            public int Level { get; set; }

            public virtual CompLevel CompLevelLink { get; set; }
        }

    }

}

