using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dawnx.AspNetCore.LiveAccount
{
    public class LiveAction : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(127)]
        public string Area { get; set; }

        [StringLength(127)]
        public string Controller { get; set; }

        [StringLength(127)]
        public string Action { get; set; }

        public bool IsValid { get; set; }

        public bool IsMarked { get; set; }

        public string Name => $"/ {Area} / {Controller} / {Action}";

        public virtual ICollection<LiveOperationAction> OperationActions { get; set; }

    }
}
