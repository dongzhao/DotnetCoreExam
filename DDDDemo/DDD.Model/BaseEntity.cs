using MediatR;


namespace DDD.Model
{
    public abstract class BaseEntity
    {
        private List<INotification> _domainEvents;
        private List<INotification> DomainEvents => this._domainEvents ?? (this._domainEvents = new List<INotification>());

        public void AddDomainEvent(INotification notification)
        {
            this.DomainEvents.Add(notification);
        }

        public void RemoveDomainEvent(INotification notification)
        {
            this.DomainEvents?.Remove(notification);
        }

    }
}
