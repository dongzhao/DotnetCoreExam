using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IEntity<ID>
    {
        ID Id { get; set; }
        /// <summary>
        /// Define thread-safe domain event collection
        /// </summary>
        IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }
    }
}
