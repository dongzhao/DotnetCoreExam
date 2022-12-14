using MediatR;


namespace DDD.Model
{
    public abstract class BaseEntity
    {
        private List<INotification> _domainEvents;
        private List<INotification> DomainEvents => _domainEvents ?? (_domainEvents = new List<INotification>());

        public void AddDomainEvent(INotification notification)
        {
            this._domainEvents.Add(notification);
        }

        public void RemoveDomainEvent(INotification notification)
        {
            this._domainEvents?.Remove(notification);
        }

    }
}
