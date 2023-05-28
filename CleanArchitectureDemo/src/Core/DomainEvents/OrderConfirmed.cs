using Core.Entities;
using Shared.Interfaces;

namespace Core.DomainEvents
{
    /// <summary>
    /// An order confirmed notification event without receiving any data back
    /// </summary>
    public class OrderConfirmed : IDomainEvent
    {
        public Order Order { get; set; }
        public OrderConfirmed(Order order)
        {
            this.Order = order;
        }
    }
}
