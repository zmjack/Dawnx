using System.ComponentModel.DataAnnotations;

namespace Dawnx.Data
{
    public abstract class RegistryStore
    {
        public string Item { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
