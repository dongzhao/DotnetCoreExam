using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Model.Entities
{
    [Table("OrderItem")]
    public class OrderItem : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Catalog")]
        public int CatalogId { get; set; }
        public virtual Catalog Catalog { get; set; } = new Catalog();

    }
}
