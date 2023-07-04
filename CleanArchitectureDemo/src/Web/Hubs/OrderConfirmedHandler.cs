using Core.DomainEvents;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Shared.Interfaces;

namespace Web.Hubs
{
    /// <summary>
    /// A mediaR handler class to trigger signalR event hub
    /// </summary>
    public class OrderConfirmedHandler : INotificationHandler<OrderConfirmed>
    {
        private readonly IHubContext<OrderConfirmedHub> _context;

        public OrderConfirmedHandler(IHubContext<OrderConfirmedHub> ctx)
        {
            this._context = ctx;
        }
        public async Task Handle(OrderConfirmed @event, CancellationToken cancellationToken)
        {
            await _context.Clients.All.SendAsync(@event.GetType().Name, $"A new order ID: '{@event.Order.Id} has been created!", cancellationToken);
        }
    }
}
