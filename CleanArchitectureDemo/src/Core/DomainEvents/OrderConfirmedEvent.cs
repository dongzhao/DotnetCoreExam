using Core.Entities;
using MediatR;

namespace Core.DomainEvents
{
    /// <summary>
    /// An order confirmed notification event without receiving any data back
    /// </summary>
    public class OrderConfirmedEvent : INotification
    {
        public Order Order { get; set; }
        public OrderConfirmedEvent(Order order)
        {
            this.Order = order;
        }
    }
}
