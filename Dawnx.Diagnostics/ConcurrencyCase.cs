using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Dawnx.Diagnostics
{
    public abstract class ConcurrencyCase<TTaskRet>
    {
        public string Name { get; }
        public abstract int Level { get; }
        public abstract int ThreadCount { get; }
        public abstract bool Validate(ConcurrentDictionary<ConcurrencyResultId, TTaskRet> result);

        public ConcurrencyCase(string name) { Name = name; }

        public ConcurrencyCaseResult<TTaskRet> Run(Func<TTaskRet> task)
        {
            var watch = new Stopwatch();
            watch.Start();
            var taskReturns = Concurrency.Run(task, Level, ThreadCount);
            watch.Stop();
            return new ConcurrencyCaseResult<TTaskRet>(Validate(taskReturns), taskReturns, watch.ElapsedMilliseconds);
        }

        public ConcurrencyCaseResult<TTaskRet> Run(Concurrency.FuncDelegate<TTaskRet> task)
        {
            var watch = new Stopwatch();
            watch.Start();
            var taskReturns = Concurrency.Run(task, Level, ThreadCount);
            watch.Stop();
            return new ConcurrencyCaseResult<TTaskRet>(Validate(taskReturns), taskReturns, watch.ElapsedMilliseconds);
        }
    }
}
