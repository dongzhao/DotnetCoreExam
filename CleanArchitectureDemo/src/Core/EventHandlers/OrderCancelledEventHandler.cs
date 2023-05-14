using Core.DomainEvents;
using MediatR;

namespace Core.EventHandlers
{
    public class OrderCancelledEventHandler : INotificationHandler<OrderCancelledEvent>
    {
        public Task Handle(OrderCancelledEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Order Cancelled...");
            return Task.CompletedTask;
        }
    }
}
