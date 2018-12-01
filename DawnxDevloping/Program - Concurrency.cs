#if USE
using Dawnx.Diagnostics;
using Dawnx.Generators;
using Dawnx.Patterns;
using System;
using System.Linq;
using System.Threading;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            G1(2000);
            G2(800000);
            G3(800000);
        }

        static void G1(int level)
        {
            using (var probe = PerformanceProbe.Create(nameof(G1)))
            {
                var ret = Concurrency.Run((runId) =>
                {
                    lock ("aa")
                    {
                        Thread.Sleep(1);
                        return DateTime.Now.Ticks.ToString();
                    }
                }, level);

                Console.WriteLine(ret.Count);
                Console.WriteLine(ret.Select(x => x.Value).Distinct().Count());
            }
        }

        static string G2_prev;
        static void G2(int level)
        {
            using (var probe = PerformanceProbe.Create(nameof(G2)))
            {
                var ret = Concurrency.Run((runId) =>
                {
                    lock ("aa")
                    {
                        var g = UseSpinLock.Do(task: () =>
                        {
                            return DateTime.Now.Ticks.ToString();
                        }, until: x => x != G2_prev);

                        G2_prev = g;
                        return g;
                    }
                }, level);

                Console.WriteLine(ret.Count);
                Console.WriteLine(ret.Select(x => x.Value).Distinct().Count());
            }
        }

        static void G3(int level)
        {
            var generator = new IdGenerator(() => DateTime.Now.Ticks.ToString());
            using (var probe = PerformanceProbe.Create(nameof(G3)))
            {
                var ret = Concurrency.Run((runId) =>
                {
                    return generator.TakeOne();
                }, level);

                Console.WriteLine(ret.Count);
                Console.WriteLine(ret.Select(x => x.Value).Distinct().Count());
            }

        }

    }
}
#endif
