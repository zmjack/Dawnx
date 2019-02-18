#if !USE
using System.Linq;
using System.Collections.Generic;
using System;
using Dawnx.Diagnostics;
using Dawnx.Con;
using System.Threading;
using Dawnx;

namespace DawnxDevloping
{
    class Program
    {
        public class SCache : Cached<string>
        {
            public SCache() : this(TimeSpan.FromSeconds(2))
            {
            }

            public SCache(TimeSpan cachePeriod) : base(cachePeriod)
            {
            }

            public override string CacheNewModel()
            {
                return DateTime.Now.ToString();
            }
        }

        static void Main(string[] args)
        {
            SCache cached = new SCache();
            var random = new Random();

            var ret = Concurrency.Run((cid) =>
            {
                Thread.Sleep(random.Next(2000));
                return cached.Get();
            }, 100, 50).ToArray();
        }

    }
}
#endif
