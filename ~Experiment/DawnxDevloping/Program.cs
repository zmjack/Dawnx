using Dawnx.Diagnostics;
using System;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var probe = PerformanceProbe.Create("id"))
            {
                Concurrency.Run((runId) =>
                {
                    Console.WriteLine(DateTime.Now.Ticks);
                }, level: 50);
            }

        }
    }
}
