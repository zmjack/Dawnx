using Dawnx.Diagnostics;
using Dawnx.Lock;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace Dawnx.Test.Lock
{
    public class InstanceLockTest
    {
        public enum Sex { Male, Female }

        public class Model
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public object Obj { get; set; }
            public Sex Sex { get; set; }
        }

        [Fact]
        public void Test()
        {
            var ModelLock_YearMonth = new InstanceLock<Model>(x => x.Year, x => x.Month, x => x.Sex);
            var model = new Model { Year = 2012, Month = 4, };

            using (var probe = PerformanceProbe.Create())
            {
                var result = Concurrency.Run(resultId =>
                {
                    try
                    {
                        using (ModelLock_YearMonth.Begin(model, 500))
                        {
                            Thread.Sleep(1000);
                            return "Entered";
                        }
                    }
                    catch (SynchronizationLockException) { return "Exception"; }
                }, level: 2, threadCount: 2);

                Assert.Equal(1, result.Values.Count(x => x == "Entered"));
                Assert.Equal(1, result.Values.Count(x => x == "Exception"));
                Assert.True(probe.ElapsedMilliseconds < 2000);
            }
        }

        [Fact]
        public void TestWrongInitialize()
        {
            Assert.ThrowsAny<ArgumentException>(() => new InstanceLock<Model>(x => x.Year, x => x.Obj));
        }

    }
}
