using MediatR;

namespace DDD.Model.DomainEvents
{
    public class OrderCancelledEventHandler : INotificationHandler<OrderCancelledEvent>
    {
        public Task Handle(OrderCancelledEvent notification, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            Console.WriteLine("Order Cancelled...");
            return Task.CompletedTask;
        }
    }
}
