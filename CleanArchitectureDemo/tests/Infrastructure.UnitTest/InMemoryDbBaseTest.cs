using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.UnitTest
{
    public abstract class InMemoryDbBaseTest
    {
        protected IServiceProvider _serviceProvider;
        protected readonly IServiceCollection _serviceCollection;
        protected IUnitOfWork _unitOfWork;

        public InMemoryDbBaseTest()
        {
            this._serviceCollection = new ServiceCollection() as IServiceCollection;

            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.test.json", optional: true, reloadOnChange: true)
                .Build();

            _serviceCollection.AddDbContext<AppDbContext>(d => d.UseInMemoryDatabase(nameof(AppDbContext)));

            _serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            _serviceCollection.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));

            _serviceCollection.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddDebug();
                builder.AddConsole();
            });


            //_serviceProvider = _serviceCollection.BuildServiceProvider();


        }
    }

}
