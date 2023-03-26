using DDD.Model.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Model.DomainEvents
{
    /// <summary>
    /// An order shipped notification event without receiving any data back
    /// </summary>
    public class OrderShippedEvent : INotification
    {
        public Order Order { get; set; }
        public OrderShippedEvent(Order order)
        {
            this.Order = order;
        }
    }
}
