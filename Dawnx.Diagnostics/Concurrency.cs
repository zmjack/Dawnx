using Dawnx.Patterns;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Dawnx.Diagnostics
{
    public class Concurrency
    {
        public static ConcurrentDictionary<string, int> Run(
            Action<string> task,
            int concurrencyLevel = 1)
        {
            return Run(i => { task(i); return 0; }, concurrencyLevel);
        }
        public static ConcurrentDictionary<string, TRet> Run<TRet>(
            Func<string, TRet> task,
            int concurrencyLevel = 1)
        {
            var ret = new ConcurrentDictionary<string, TRet>();

            if (concurrencyLevel < 1)
                throw new ArgumentException("The `level` must be greater than 0.");

            var div = concurrencyLevel / Environment.ProcessorCount;
            var mod = concurrencyLevel % Environment.ProcessorCount;

            var threadCount = Math.Min(concurrencyLevel, Environment.ProcessorCount);

            var threads = new Thread[threadCount];
            foreach (var tid in Range.Create(threadCount))
            {
                threads[tid] = new Thread(() =>
                {
                    var s_count = tid < mod ? div + 1 : div;
                    foreach (var tsid in Range.Create(s_count))
                    {
                        var id = $"{tid}:{tsid}";
                        var taskRet = task(id);
                        ret.GetOrAdd(id, taskRet);
                    }
                });
            }

            foreach (var thread in threads)
                thread.Start();

            UseSpinLock.Do(
                task: () => { },
                until: () => ret.Count == concurrencyLevel);

            return ret;
        }

    }
}
