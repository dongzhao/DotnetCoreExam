using DDD.Model.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Model.DomainEvents
{
    public class OrderConfirmedEvent : INotification
    {
        public Order Order { get; set; }
        public OrderConfirmedEvent(Order order)
        {
            this.Order = order;
        }
    }
}
