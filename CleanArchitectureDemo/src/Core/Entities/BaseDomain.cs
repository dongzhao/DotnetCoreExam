using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public abstract class BaseDomain
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
