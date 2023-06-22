
using Core.DomainEvents;
using Core.Entities;
using Core.Enums;
using Core.Values;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.UnitTest
{
    public class DomainEventTest
    {
        protected IServiceProvider _serviceProvider;
        protected readonly IServiceCollection _serviceCollection;
        public DomainEventTest()
        {
            this._serviceCollection = new ServiceCollection() as IServiceCollection;
            _serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }
    }

    public class EmailNotification : INotification
    {
        public EmailNotification()
        {
        }
    }

}