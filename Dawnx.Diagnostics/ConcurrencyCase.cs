using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Dawnx.Diagnostics
{
    public abstract class ConcurrencyCase<TTaskRet>
    {
        public abstract int Level { get; }
        public abstract int ThreadCount { get; }
        public abstract bool Validate(ConcurrentDictionary<ConcurrencyResultId, TTaskRet> result);

        public virtual void BeforeRun() { }
        public virtual void Complete() { }

        public ConcurrencyCaseResult<TTaskRet> Run(Func<ConcurrencyResultId, TTaskRet> task)
        {
            ConcurrentDictionary<ConcurrencyResultId, TTaskRet> taskReturns;

            var watch = new Stopwatch();
            BeforeRun();
            watch.Start();
            taskReturns = Concurrency.Run(task, Level, ThreadCount);
            watch.Stop();

            return new ConcurrencyCaseResult<TTaskRet>(Validate(taskReturns), taskReturns, watch.Elapsed);
        }
    }
}
