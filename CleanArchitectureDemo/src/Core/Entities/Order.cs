
using Core.DomainEvents;
using Core.Enums;
using Core.Values;
using Shared.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Order")]
    public class Order : BaseEntity<int>
    {
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

        public void AddToEvents()
        {
            this.PublishEvent(new OrderConfirmed(this));
        }
    }
}
