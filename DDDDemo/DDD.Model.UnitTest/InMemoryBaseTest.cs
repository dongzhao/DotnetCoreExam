using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Model.UnitTest
{
    public abstract class InMemoryBaseTest
    {
        protected readonly IServiceProvider _serviceProvider;
        public InMemoryBaseTest()
        {
            var services = new ServiceCollection() as IServiceCollection;
            services.AddDbContext<ShoppingCartDbContext>(d => d.UseInMemoryDatabase(nameof(ShoppingCartDbContext)));
            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
