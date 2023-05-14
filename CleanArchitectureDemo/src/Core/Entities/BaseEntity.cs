using MediatR;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public abstract class BaseEntity<ID> : BaseDomain, IEntity<ID>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ID Id { get; set; }

        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public virtual string CreatedBy { get; set; } = string.Empty;

        public virtual DateTime ModifiedDateTime { get; set; }

        public virtual string ModifiedBy { get; set; } = string.Empty;

    }
}
