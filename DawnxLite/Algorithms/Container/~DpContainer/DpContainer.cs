namespace Dawnx.Algorithms.Container
{
    public abstract partial class DpContainer<TIn, TOut>
    {
        public abstract TOut StateTransfer(TIn x);
    }
}
