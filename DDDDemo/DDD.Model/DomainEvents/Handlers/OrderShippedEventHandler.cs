using MediatR;

namespace DDD.Model.DomainEvents
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
