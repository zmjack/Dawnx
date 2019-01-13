#if USE
using Dawnx.Diagnostics;
using Dawnx.Net.Http;
using SimpleData;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
                var s = sqlite.Order_Details.Average(x => x.Quantity);
                Console.WriteLine();
            }
        }

    }
}
#endif
