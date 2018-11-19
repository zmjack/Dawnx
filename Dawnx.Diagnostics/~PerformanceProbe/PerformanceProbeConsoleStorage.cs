using System;

namespace Dawnx.Diagnostics
{
    public class PerformanceProbeConsoleStorage : PerformanceProbeStorage<string>
    {
        public override void StorePerformanceData(string carry, string filePath, int lineNumber, string memberName, long elapsedMilliseconds)
        {
            Console.WriteLine();
            Console.WriteLine($"{filePath}{Environment.NewLine}" +
                $"  Line:\t{lineNumber}\t" +
                $"Caller:\t{memberName}\t" +
                $"Carry:\t{carry}\t" +
                $"ElapsedTime:\t{TimeSpan.FromMilliseconds(elapsedMilliseconds)}");
        }
    }

}
