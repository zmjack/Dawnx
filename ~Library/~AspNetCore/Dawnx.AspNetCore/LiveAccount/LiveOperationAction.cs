using Dawnx.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dawnx.AspNetCore.LiveAccount
{
    public class LiveOperationAction : IEntity
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
