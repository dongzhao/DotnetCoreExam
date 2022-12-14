using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DDD.Model.UnitTest
{
    public abstract class DomainEventBaseTest
    {
        protected readonly IServiceProvider _serviceProvider;
        public DomainEventBaseTest()
        {
            var services = new ServiceCollection() as IServiceCollection;
            services.AddDbContext<ShoppingCartDbContext>(d => d.UseInMemoryDatabase(nameof(ShoppingCartDbContext)));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
