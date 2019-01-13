using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dawnx.AspNetCore.LiveRegistry.Entities
{
    public class AppRegistry
    {
        [Key]
        public Guid Id { get; set; }

        public AppRegistryScope Scope { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public bool IsValid { get; set; }

    }
}
