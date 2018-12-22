using System;
using System.Collections.Concurrent;

namespace Dawnx.Diagnostics
{
    public class ConcurrencyCaseResult<TTaskRet>
    {
        public bool IsValid { get; }
        public ConcurrentDictionary<ConcurrencyResultId, TTaskRet> TaskResults { get; }
        public TimeSpan Elapsed { get; }

        public ConcurrencyCaseResult(bool isValid, ConcurrentDictionary<ConcurrencyResultId, TTaskRet> taskResults, TimeSpan elapsed)
        {
            IsValid = isValid;
            TaskResults = taskResults;
            Elapsed = elapsed;
        }
    }
}
