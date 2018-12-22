using System.Collections.Concurrent;

namespace Dawnx.Diagnostics
{
    public class ConcurrencyCaseResult<TTaskRet>
    {
        public bool IsValid { get; }
        public ConcurrentDictionary<ConcurrencyResultId, TTaskRet> TaskResults { get; }
        public long ElapsedMilliseconds { get; }

        public ConcurrencyCaseResult(bool isValid, ConcurrentDictionary<ConcurrencyResultId, TTaskRet> taskResults, long elapsedMilliseconds)
        {
            IsValid = isValid;
            TaskResults = taskResults;
            ElapsedMilliseconds = elapsedMilliseconds;
        }
    }
}
