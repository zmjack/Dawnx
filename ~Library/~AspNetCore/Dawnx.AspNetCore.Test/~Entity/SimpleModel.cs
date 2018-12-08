using Dawnx.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dawnx.AspNetCore.Test
{
    public class SimpleModel : IEntity<SimpleModel>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }

    }
}
