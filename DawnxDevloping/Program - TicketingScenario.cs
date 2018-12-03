using Dawnx;
using Dawnx.Diagnostics;
using Dawnx.Generators;
using Dawnx.Patterns;
using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace DawnxDevloping
{
    class Program
    {
        static string NewConstString => new StringBuilder().Self(_ => _.Append("a")).ToString();
        static int RemainingTicket = 1_000_000;

        static void Main(string[] args)
        {
            Sell(1_000_000);
            Console.WriteLine(RemainingTicket);
        }

        static void Sell(int level)
        {
            using (var probe = PerformanceProbe.Create(nameof(Sell)))
            {
                var ret = Concurrency.Run((runId) =>
                {
                    lock (Locker.Get(NewConstString))
                    {
                        return RemainingTicket--;
                    }
                }, level);
            }
        }

    }
}
