using Dawnx.Patterns;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Dawnx.Diagnostics
{
    public class Concurrency
    {
        public delegate void TaskDelegate(string runId);
        public delegate TRet FuncDelegate<TRet>(string runId);

        /// <summary>
        /// Use mutil-thread to simulate concurrent scenarios.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="level"></param>
        /// <param name="threadCount">If the value is 0, <see cref="Environment.ProcessorCount"/> will be used.</param>
        /// <returns></returns>
        public static ConcurrentDictionary<string, int> Run(
            TaskDelegate task,
            int level = 1,
            int threadCount = 0)
        {
            return Run(i => { task(i); return 0; }, level);
        }

        /// <summary>
        /// Use mutil-thread to simulate concurrent scenarios.
        /// </summary>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="task"></param>
        /// <param name="level"></param>
        /// <param name="threadCount">If the value is 0, <see cref="Environment.ProcessorCount"/> will be used.</param>
        /// <returns></returns>
        public static ConcurrentDictionary<string, TRet> Run<TRet>(
            FuncDelegate<TRet> task,
            int level = 1,
            int threadCount = 0)
        {
            if (level < 1)
                throw new ArgumentException("The `level` must be greater than 0.");

            if (threadCount == 0)
                threadCount = Environment.ProcessorCount;

            var div = level / threadCount;
            var mod = level % threadCount;
            threadCount = Math.Min(level, threadCount);

            var ret = new ConcurrentDictionary<string, TRet>();

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
                until: () => ret.Count == level);

            return ret;
        }

    }
}
