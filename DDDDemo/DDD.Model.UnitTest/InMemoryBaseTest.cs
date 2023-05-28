using DDD.Model.Entities;
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

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<typeof(IRepository<>), typeof(BaseRepository<>)>();
            services.AddTransient<IRepository<Order, int>, BaseRepository<Order, int>>();
            services.AddTransient<IRepository<OrderItem, int>, BaseRepository<OrderItem, int>>();
            services.AddTransient<IRepository<Catalog, int>, BaseRepository<Catalog, int>>();

            _serviceProvider = services.BuildServiceProvider();


        }
    }
}
