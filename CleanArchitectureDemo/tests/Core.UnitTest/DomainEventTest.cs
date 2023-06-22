
using Core.DomainEvents;
using Core.Entities;
using Core.Enums;
using Core.EventHandlers;
using Core.Values;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.UnitTest
{
    public class EmailEvent : INotification
    {

    }

    public class EmailHandler : INotificationHandler<EmailEvent>
    {
        public Task Handle(EmailEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("OK");
        }
    }

    public class DomainEventTest 
    {
        protected IServiceProvider _serviceProvider;
        private IMediator _mediator;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection() as IServiceCollection;
            services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<INotificationHandler<EmailEvent>, EmailHandler>();

            _serviceProvider = services.BuildServiceProvider();

            this._mediator = _serviceProvider.GetService(typeof(IMediator)) as IMediator;
            //var handler = _serviceProvider.GetService(typeof(EmailHandler));
            
            //Assert.IsNotNull(handler);
        }

        [Test]
        public async Task test_order_confirmed()
        {
            var emailEvent = new EmailEvent();
            await _mediator.Publish(emailEvent);
            

            Assert.Pass("OK");

        }
    }

}