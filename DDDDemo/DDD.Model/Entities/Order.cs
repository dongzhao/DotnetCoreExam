using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Model.Entities
{
    [Table("Order")]
    public class Order : IEntity<int>
    {
        [Key]
        public int Id { get; set ; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
