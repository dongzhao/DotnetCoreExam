using Core.Entities;
using Shared.Interfaces;

namespace Core.DomainEvents
{
    /// <summary>
    /// An order shipped notification event without receiving any data back
    /// </summary>
    public class OrderShipped : IDomainEvent
    {
        public Order Order { get; set; }
        public OrderShipped(Order order)
        {
            this.Order = order;
        }
    }
}
