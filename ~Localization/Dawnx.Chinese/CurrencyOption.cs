namespace Dawnx.Chinese
{
    public class CurrencyOption
    {
        public enum ETarget { Lower, Upper }

        public bool IsSimplified { get; set; }
        public ETarget Target { get; set; }
    }
}
