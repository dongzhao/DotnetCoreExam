
using Core.DomainEvents;
using Core.Entities;
using Core.Enums;
using Core.EventHandlers;
using Core.Values;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UnitTest
{ 
    public class OrderEventTest : DomainEventBaseTest
    {
        private IMediator _mediator;

        [SetUp]
        public void Setup()
        {
             this._mediator = _serviceProvider.GetService(typeof(IMediator)) as IMediator;
            var handler = _serviceProvider.GetService(typeof(OrderConfirmedHandler));

            Assert.NotNull(handler);
        }

        [Test]
        public async Task test_order_confirmed()
        {
            var orderConfirmedEvent = new OrderConfirmed(new Order());
            await _mediator.Publish(orderConfirmedEvent);
            

            Assert.Pass("OK");

        }
    }

}