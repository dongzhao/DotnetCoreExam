using Core.DomainEvents;
using MediatR;

namespace Core.EventHandlers
{
    public class OrderShippedHandler : INotificationHandler<OrderShipped>
    {
        public Task Handle(OrderShipped notification, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();

            Console.WriteLine("Order shipped...");
            return Task.CompletedTask;
        }
    }
}
