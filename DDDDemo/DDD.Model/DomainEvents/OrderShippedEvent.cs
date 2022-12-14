using DDD.Model.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Model.DomainEvents
{
    public class OrderShippedEvent : INotification
    {
        public Order Order { get; set; }
        public OrderShippedEvent(Order order)
        {
            this.Order = order;
        }
    }
}
