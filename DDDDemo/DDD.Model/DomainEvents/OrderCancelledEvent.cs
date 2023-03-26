using DDD.Model.Entities;
using MediatR;

namespace DDD.Model.DomainEvents
{
    /// <summary>
    /// An order cancelled notification event without receiving any data back
    /// </summary>
    public class OrderCancelledEvent : INotification
    {
        public Order Order { get; set; }
        public OrderCancelledEvent(Order order)
        {
            this.Order = order;
        }
    }
}
