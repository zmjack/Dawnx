#if USE
using Dawnx.Diagnostics;
using Dawnx.Generators;
using Dawnx.Patterns;
using System;
using System.Linq;
using System.Threading;

namespace DawnxDevloping
{
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> Lazied =
            new Lazy<Singleton>(() => new Singleton());

        public static Singleton Instance => Lazied.Value;

        private Singleton()
        {
            Console.WriteLine("C");
        }

        public void Print()
        {
            Console.WriteLine(GetHashCode());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            G1(100);
        }

        static void G1(int level)
        {
            using (var probe = PerformanceProbe.Create(nameof(G1)))
            {
                var ret = Concurrency.Run<int>((cid) =>
                {
                    Singleton.Instance.Print();
                    return 0;
                }, level);

                Console.WriteLine(ret.Count);
                Console.WriteLine(ret.Select(x => x.Value).Distinct().Count());
            }
        }

    }
}
#endif
