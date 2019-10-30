using Dawnx.Diagnostics;
using Dawnx.Lock;
using System.Linq;
using System.Threading;
using Xunit;

namespace Dawnx.Test.Lock
{
    public class TypeLockTest
    {
        [Fact]
        public void Test()
        {
            using (var probe = PerformanceProbe.Create())
            {
                var result = Concurrency.Run(resultId =>
                {
                    try
                    {
                        using (TypeLock<TypeLockTest>.Get("").Begin(500))
                        {
                            Thread.Sleep(1000);
                            return "Entered";
                        }
                    }
                    catch (SynchronizationLockException) { return "Exception"; }
                }, level: 2, threadCount: 2);

                Assert.Equal(1, result.Values.Count(x => x == "Entered"));
                Assert.Equal(1, result.Values.Count(x => x == "Exception"));
                Assert.True(probe.ElapsedMilliseconds < 1900);
            }
        }

    }
}
