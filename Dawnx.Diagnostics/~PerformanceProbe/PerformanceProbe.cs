using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Dawnx.Diagnostics
{
    public class PerformanceProbe : PerformanceProbe<PerformanceProbeConsoleStorage, string>
    {
        protected PerformanceProbe(string carry, string memberName, string filePath, int lineNumber)
            : base(carry, filePath, lineNumber, memberName)
        {
        }
    }

    public class PerformanceProbe<TStorage, TCarryObject> : IDisposable
        where TStorage : PerformanceProbeStorage<TCarryObject>, new()
        where TCarryObject : class
    {
        private Stopwatch _watch = new Stopwatch();
        private TStorage _storage = new TStorage();

        public TCarryObject Carry { get; }
        public string CallerMemberName { get; }
        public string CallerFilePath { get; }
        public int CallerLineNumber { get; }
        public long ElapsedMilliseconds => _watch.ElapsedMilliseconds;

        protected PerformanceProbe(TCarryObject carry, string callerFilePath, int callerLineNumber, string callerMemberName)
        {
            Carry = carry;
            CallerFilePath = callerFilePath;
            CallerMemberName = callerMemberName;
            CallerLineNumber = callerLineNumber;
            _watch.Start();
        }

        public static PerformanceProbe<TStorage, TCarryObject> Create(
            TCarryObject carry = null,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "")
            => new PerformanceProbe<TStorage, TCarryObject>(carry, callerFilePath, callerLineNumber, callerMemberName);

        public void Dispose()
        {
            _watch.Stop();
            _storage.StorePerformanceData(Carry, CallerFilePath, CallerLineNumber, CallerMemberName, ElapsedMilliseconds);
        }

    }
}
