using Core.Entities;
using MediatR;

namespace Core.DomainEvents
{
    /// <summary>
    /// An order shipped notification event without receiving any data back
    /// </summary>
    public class OrderShippedEvent : INotification
    {
        public Order Order { get; set; }
        public OrderShippedEvent(Order order)
        {
            this.Order = order;
        }
    }
}
