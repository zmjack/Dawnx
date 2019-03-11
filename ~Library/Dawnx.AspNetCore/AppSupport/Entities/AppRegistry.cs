﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Dawnx.AspNetCore.AppSupport.Entities
{
    public class AppRegistry
    {
        [Key]
        public Guid Id { get; set; }

        public AppRegistryScope Scope { get; set; }

        [StringLength(255)]
        public string Group { get; set; }

        [StringLength(255)]
        public string Key { get; set; }

        [StringLength(255)]
        public string Value { get; set; }

        public bool IsValid { get; set; }

    }
}
