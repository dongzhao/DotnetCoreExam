using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Interfaces;


namespace Infrastructure.Data
{
    public class MediatorDomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MediatorDomainEventDispatcher> _log;
        public MediatorDomainEventDispatcher(IMediator mediator, ILogger<MediatorDomainEventDispatcher> log)
        {
            _mediator = mediator;
            _log = log;
        }

        public async Task Dispatch(IDomainEvent domainEvent)
        {
            //var domainEventNotification = _createDomainEventNotification(domainEvent);
            var domainEventNotification = domainEvent as INotification;
            _log.LogDebug($"Dispatching notification...  EventType: {domainEvent.GetType()}");
            await _mediator.Publish(domainEventNotification);
        }

        //private INotification _createDomainEventNotification(IDomainEvent domainEvent)
        //{
        //    var genericDispatcherType = typeof(IDomainEvent).MakeGenericType(domainEvent.GetType());
        //    return (INotification)Activator.CreateInstance(genericDispatcherType, domainEvent);
        //}
    }
}
