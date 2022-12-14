using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Model.UnitTest
{
    public abstract class SqlServerBaseTest
    {
        protected readonly IServiceProvider _serviceProvider;
        public SqlServerBaseTest()
        {
            var services = new ServiceCollection() as IServiceCollection;
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.test.json", optional: true, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfiguration>(config);
            services.AddDbContext<ShoppingCartDbContext>(d => d.UseSqlServer(config.GetConnectionString(nameof(ShoppingCartDbContext))));
            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
