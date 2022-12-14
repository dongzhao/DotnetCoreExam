using DDD.Model.Enums;
using DDD.Model.Values;
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
    public class Order : BaseEntity, IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set ; }
        public DateTime CurrentDateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Enum mapping:
        /// int or char value stored in database 
        /// </summary>
        public OrderStatus Status { get; set; }
        public Address? ShippingAddress { get; set; } = new Address();
        public Address? ResidentAddress { get; set; } = new Address();
        public Recipient? Recipient { get; set; } = new Recipient();
        public CreditCard? CreditCard { get; set; } = new CreditCard();
        /// <summary>
        /// Enum mapping:
        /// Enum string store in database
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public CardType CardType { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
