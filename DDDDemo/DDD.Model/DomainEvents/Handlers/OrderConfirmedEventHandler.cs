using MediatR;

namespace DDD.Model.DomainEvents
{
    public class OrderConfirmedEventHandler : INotificationHandler<OrderConfirmedEvent>
    {
        public Task Handle(OrderConfirmedEvent notification, CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();
            Console.WriteLine("Order Confirmed");
            return Task.CompletedTask;
        }
    }
}
