using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dawnx.Data
{
    public abstract class RegistryStore
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Item { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
