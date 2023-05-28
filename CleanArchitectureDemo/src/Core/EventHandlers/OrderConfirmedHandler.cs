using Core.DomainEvents;
using MediatR;

namespace Core.EventHandlers
{
    public class OrderConfirmedHandler : INotificationHandler<OrderConfirmed>
    {
        public Task Handle(OrderConfirmed notification, CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();
            Console.WriteLine("Order Confirmed");
            return Task.CompletedTask;
        }
    }
}
