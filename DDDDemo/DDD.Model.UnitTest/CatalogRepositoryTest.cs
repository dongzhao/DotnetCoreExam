using DDD.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDD.Model.UnitTest
{
    public class CatalogRepositoryInMemoryTest : InMemoryBaseTest
    {
        private ShoppingCartDbContext _ctx ;
        private IUnitOfWork _unitOfWork ;
        private IRepository<Catalog, int> catalogRepository; 

        [SetUp]
        public void Setup()
        {
            this._unitOfWork = _serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            this.catalogRepository = _unitOfWork
        }

        [TestCase("Oppo A96", 399.55)]
        public void test_create(string name, decimal price)
        {
            var expected = new Catalog()
            {
                Name = name,
                Price = price,
            };

            _ctx.CatalogSet.Add(expected);
            _ctx.SaveChanges();
            Assert.IsTrue(expected.Id > 0);

            var actural = _ctx.CatalogSet.SingleOrDefault(x => x.Id == expected.Id);
            Assert.That(actural.Name, Is.EqualTo(name));
            Assert.That(actural.Price, Is.EqualTo(price));

        }
    }

    public class CatalogSqlServerTest : SqlServerBaseTest
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
        [TestCase("Oppo A96", 399.55)]
        public void test_create(string name, decimal price)
        {
            var expected = new Catalog()
            {
                Name = name,
                Price = price,
            };

            _ctx.CatalogSet.Add(expected);
            _ctx.SaveChanges();
            Assert.IsTrue(expected.Id > 0);

            var actural = _ctx.CatalogSet.SingleOrDefault(x => x.Id == expected.Id);
            Assert.That(actural.Name, Is.EqualTo(name));
            Assert.That(actural.Price, Is.EqualTo(price));

        }
    }

}