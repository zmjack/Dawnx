#if USE
using Dawnx.Diagnostics;
using Dawnx.Generators;
using Dawnx.Patterns;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            Test(2000);
        }

        static void Test(int level)
        {
            var ret = Concurrency.Run(cid =>
            {
                // 这里写被测试逻辑（返回值为 string）
                return DateTime.Now.Ticks.ToString();
            }, level);
        }

    }
}
#endif
