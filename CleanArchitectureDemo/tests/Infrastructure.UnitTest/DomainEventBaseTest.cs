using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;
using System.Reflection;

namespace Infrastructure.UnitTest
{
    public abstract class DomainEventBaseTest : InMemoryDbBaseTest
    {
        public DomainEventBaseTest() : base()
        {
            // add MediatR dependency injection
            _serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            _serviceCollection.AddScoped<IDomainEventDispatcher, MediatorDomainEventDispatcher>();
        }

        [SetUp]
        public void Setup()
        {
            this._serviceProvider = _serviceCollection.BuildServiceProvider();
            this._unitOfWork = _serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
        }
    }

}
