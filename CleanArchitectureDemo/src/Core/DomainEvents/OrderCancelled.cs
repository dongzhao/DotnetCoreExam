using Core.Entities;
using Shared.Interfaces;

namespace Core.DomainEvents
{
    /// <summary>
    /// An order cancelled notification event without receiving any data back
    /// </summary>
    public class OrderCancelled : IDomainEvent
    {
        public Order Order { get; set; }

        public OrderCancelled(Order order)
        {
            this.Order = order;
        }
    }
}
