using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.UnitTest
{
    public abstract class DomainEventBaseTest
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly TestDbContext _ctx;

        public DomainEventBaseTest()
        {
            var services = new ServiceCollection() as IServiceCollection;
            services.AddDbContext<TestDbContext>(d => d.UseInMemoryDatabase(nameof(TestDbContext)));

            //services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            _serviceProvider = services.BuildServiceProvider();

            _ctx = _serviceProvider.GetService(typeof(TestDbContext)) as TestDbContext;
            //_ctx.Database.EnsureDeleted();
            //_ctx.Database.EnsureCreated();

        }
    }

}
