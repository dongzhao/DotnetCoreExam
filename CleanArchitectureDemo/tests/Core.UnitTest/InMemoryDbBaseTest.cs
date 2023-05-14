using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UnitTest
{
    public abstract class InMemoryDbBaseTest
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly TestDbContext _ctx;

        public InMemoryDbBaseTest()
        {
            var services = new ServiceCollection() as IServiceCollection;
            services.AddDbContext<TestDbContext>(d => d.UseInMemoryDatabase(nameof(TestDbContext)));

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            ////services.AddScoped<typeof(IRepository<>), typeof(BaseRepository<>)>();
            //services.AddTransient<IRepository<Order, int>, BaseRepository<Order, int>>();
            //services.AddTransient<IRepository<OrderItem, int>, BaseRepository<OrderItem, int>>();
            //services.AddTransient<IRepository<Catalog, int>, BaseRepository<Catalog, int>>();

            _serviceProvider = services.BuildServiceProvider();

            _ctx = _serviceProvider.GetService(typeof(TestDbContext)) as TestDbContext;
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

        }
    }

}
