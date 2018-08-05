using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Dawnx.Diagnostics
{
    public class PerformanceProbe : PerformanceProbe<PerformanceProbeConsoleStorage, string>
    {
        protected PerformanceProbe(string carry, string memberName, string filePath, int lineNumber)
            : base(carry, memberName, filePath, lineNumber)
        {
        }
    }

    public class PerformanceProbe<TStorage, TCarryObject> : IDisposable
        where TStorage : PerformanceProbeStorage<TCarryObject>, new()
        where TCarryObject : class
    {
        private Stopwatch _watch = new Stopwatch();
        private TStorage _storage = new TStorage();

        public TCarryObject Carry { get; private set; }
        public string MemberName { get; private set; }
        public string FilePath { get; private set; }
        public int LineNumber { get; private set; }
        public long ElapsedMilliseconds => _watch.ElapsedMilliseconds;

        protected PerformanceProbe(TCarryObject carry, string memberName, string filePath, int lineNumber)
        {
            Carry = carry;
            MemberName = memberName;
            FilePath = filePath;
            LineNumber = lineNumber;
            _watch.Start();
        }

        public static PerformanceProbe<TStorage, TCarryObject> Create(
            TCarryObject carry = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
            => new PerformanceProbe<TStorage, TCarryObject>(carry, memberName, filePath, lineNumber);

        public void Dispose()
        {
            _watch.Stop();
            _storage.StorePerformanceData(Carry, FilePath, LineNumber, MemberName, ElapsedMilliseconds);
        }

    }
}
