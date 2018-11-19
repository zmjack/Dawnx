namespace Dawnx.Diagnostics
{
    public abstract class PerformanceProbeStorage<TCarryObject>
        where TCarryObject : class
    {
        public abstract void StorePerformanceData(
            TCarryObject carry,
            string filePath, int lineNumber, string memberName,
            long elapsedMilliseconds);
    }

}
