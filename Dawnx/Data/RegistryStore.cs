namespace Dawnx.Data
{
    public abstract class RegistryStore
    {
        [Key]
        public string Item { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
