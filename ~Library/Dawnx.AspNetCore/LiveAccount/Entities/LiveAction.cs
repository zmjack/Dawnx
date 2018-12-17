using Dawnx.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dawnx.AspNetCore.LiveAccount.Entities
{
    public class LiveAction : IEntity<LiveAction>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(127)]
        public string Area { get; set; }

        [StringLength(127)]
        public string Controller { get; set; }

        [StringLength(127)]
        public string Action { get; set; }

        public bool IsExisted { get; set; }

        public bool IsEnabled { get; set; }

        public string Name => $"/ {Area ?? "[NoArea]"} / {Controller ?? "[NoController]"} / {Action}";

        public virtual ICollection<LiveOperationAction> OperationActions { get; set; }

    }
}
