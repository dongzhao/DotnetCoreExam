using DDD.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDD.Model.UnitTest
{
    public class InMemoryDbContextTest : InMemoryBaseTest
    {
        private ShoppingCartDbContext _ctx ;

        [SetUp]
        public void Setup()
        {
            this._ctx = _serviceProvider.GetService(typeof(ShoppingCartDbContext)) as ShoppingCartDbContext;
        }

        [Test]
        public void sanity_check()
        {
            Assert.IsNotNull(_ctx);
        }
    }

    public class SqlServerDbContextTest : SqlServerBaseTest
    {
        private ShoppingCartDbContext _ctx;

        [SetUp]
        public void Setup()
        {
            this._ctx = _serviceProvider.GetService(typeof(ShoppingCartDbContext)) as ShoppingCartDbContext;
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
        }

        [Test]
        public void sanity_check()
        {
            Assert.IsNotNull(_ctx);
        }
    }

    public class ShoppingCartDbContext : DbContext
    {
        public virtual DbSet<Catalog> CatalogSet { get; set; }
        public virtual DbSet<Order> OrderSet { get; set; }
        public virtual DbSet<OrderItem> OrderItemSet { get; set; }

        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            builder.Entity<Order>(o => 
            {
                o.OwnsOne(c => c.ResidentAddress);
                o.OwnsOne(c => c.ShippingAddress);
                o.OwnsOne(c => c.Recipient);
                o.OwnsOne(c => c.CreditCard);
            });
        }
    }
}