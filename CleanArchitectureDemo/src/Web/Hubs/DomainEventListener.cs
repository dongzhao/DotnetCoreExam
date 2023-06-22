using Core.DomainEvents;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Shared.Interfaces;

namespace Web.Hubs
{
    public class DomainEventListener : INotificationHandler<OrderConfirmed>
    {
        private readonly IHubContext<DomainEventHub> _context;

        public DomainEventListener(IHubContext<DomainEventHub> ctx)
        {
            this._context = ctx;
        }
        public Task Handle(OrderConfirmed @event, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            // "EntityUpdated", entityName, message
            return _context.Clients.All.SendAsync("EntityUpdated", @event.GetType().Name, $"domain data {@event.GetType().Name} changed!", cancellationToken);
        }
    }
}
