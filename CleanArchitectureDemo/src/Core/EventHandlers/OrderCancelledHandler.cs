using Core.DomainEvents;
using MediatR;

namespace Core.EventHandlers
{
    public class OrderCancelledEventHandler : INotificationHandler<OrderCancelled>
    {
        public Task Handle(OrderCancelled notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Order Cancelled...");
            return Task.CompletedTask;
        }
    }
}
