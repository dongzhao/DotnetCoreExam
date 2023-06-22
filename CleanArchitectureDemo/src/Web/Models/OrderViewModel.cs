using Core.Entities;
using Core.Enums;
using Core.Values;
using Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class OrderViewModel
    {
        [Display(Name = "Order Number")]
        public int OrderId { get; set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; } = DateTime.MinValue.Date;
        [Display(Name = "Order By")]
        //[CaseSensitive]
        public string OrderBy { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
        public Address? ShippingAddress { get; set; } = new Address();
        public Address? ResidentAddress { get; set; } = new Address();
        public Recipient? Recipient { get; set; } = new Recipient();
        public CreditCard? CreditCard { get; set; } = new CreditCard();
        public CardType CardType { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
