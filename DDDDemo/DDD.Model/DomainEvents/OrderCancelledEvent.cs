using DDD.Model.Entities;
using MediatR;

namespace DDD.Model.DomainEvents
{
    public class OrderCancelledEvent : INotification
    {
        public Order Order { get; set; }
        public OrderCancelledEvent(Order order)
        {
            this.Order = order;
        }
    }
}
