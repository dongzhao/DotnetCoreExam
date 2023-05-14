using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;

namespace Infrastructure.UnitTest
{
    public abstract class InMemoryDbBaseTest
    {
        protected readonly IServiceProvider _serviceProvider;
        public InMemoryDbBaseTest()
        {
            var services = new ServiceCollection() as IServiceCollection;
            services.AddDbContext<AppDbContext>(d => d.UseInMemoryDatabase(nameof(AppDbContext)));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));


            _serviceProvider = services.BuildServiceProvider();


        }
    }

}
