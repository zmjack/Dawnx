﻿using Dawnx.Patterns;
using Dawnx.Ranges;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Dawnx.Diagnostics
{
    public class Concurrency
    {
        /// <summary>
        /// Use mutil-thread to simulate concurrent scenarios.
        /// </summary>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="task"></param>
        /// <param name="level"></param>
        /// <param name="threadCount">If the value is 0, <see cref="Environment.ProcessorCount"/> will be used.</param>
        /// <returns></returns>
        public static ConcurrentDictionary<ConcurrencyResultId, TRet> Run<TRet>(
            Func<ConcurrencyResultId, TRet> task,
            int level,
            int threadCount = 0)
        {
            if (level < 1)
                throw new ArgumentException("The `level` must be greater than 0.");

            if (threadCount == 0)
                threadCount = Environment.ProcessorCount;

            var div = level / threadCount;
            var mod = level % threadCount;
            threadCount = Math.Min(level, threadCount);

            var ret = new ConcurrentDictionary<ConcurrencyResultId, TRet>();

            var threads = new Thread[threadCount];
            foreach (var threadNumber in IntegerRange.Create(threadCount))
            {
                threads[threadNumber] = new Thread(() =>
                {
                    var s_count = threadNumber < mod ? div + 1 : div;
                    foreach (var invokeNumber in IntegerRange.Create(s_count))
                    {
                        var threadId = Thread.CurrentThread.ManagedThreadId;
                        try
                        {
                            var taskRet = task(new ConcurrencyResultId(threadId, invokeNumber));
                            ret.GetOrAdd(new ConcurrencyResultId(threadId, invokeNumber), taskRet);
                        }
                        catch
                        {
                            ret.GetOrAdd(new ConcurrencyResultId(threadId, invokeNumber), default(TRet));
                        }
                    }
                });
            }

            foreach (var thread in threads)
                thread.Start();

            UseSpinLock.Do(
                task: () => { },
                until: () => ret.Count == level,
                frequency: TimeSpan.FromSeconds(0.5));

            return ret;
        }

    }
}
