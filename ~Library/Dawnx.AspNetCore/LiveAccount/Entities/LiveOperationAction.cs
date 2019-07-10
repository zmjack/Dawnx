using NLinq;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dawnx.AspNetCore.LiveAccount.Entities
{
    public class LiveOperationAction : IEntity<LiveOperationAction>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(OperationLink))]
        public Guid Operation { get; set; }

        [ForeignKey(nameof(ActionLink))]
        public Guid Action { get; set; }

        public virtual LiveOperation OperationLink { get; set; }
        public virtual LiveAction ActionLink { get; set; }

    }
}
