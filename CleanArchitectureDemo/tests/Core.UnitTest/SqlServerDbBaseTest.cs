using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.UnitTest
{
    public abstract class SqlServerDbBaseTest
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly TestDbContext _ctx;

        public SqlServerDbBaseTest()
        {
            var services = new ServiceCollection() as IServiceCollection;
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.test.json", optional: true, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfiguration>(config);
            services.AddDbContext<TestDbContext>(d => d.UseSqlServer(config.GetConnectionString(nameof(TestDbContext))));
            _serviceProvider = services.BuildServiceProvider();

            this._ctx = _serviceProvider.GetService(typeof(TestDbContext)) as TestDbContext;
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
        }
    }
}
