using Core.DomainEvents;
using MediatR;

namespace Core.EventHandlers
{
    public class OrderShippedEventHandler : INotificationHandler<OrderShippedEvent>
    {
        public Task Handle(OrderShippedEvent notification, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            Console.WriteLine("Order shipped...");
            return Task.CompletedTask;
        }
    }
}
