
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.UnitTest
{
    public class CatalogInMemoryTest : InMemoryDbBaseTest
    {

        [TestCase("Oppo A96", 399.55)]
        public void test_create(string name, decimal price)
        {
            var expected = new Catalog()
            {
                Name = name,
                Price = price,
                CreatedBy = "TestA",
            };

            _ctx.CatalogSet.Add(expected);
            _ctx.SaveChanges();
            Assert.IsTrue(expected.Id > 0);

            var actural = _ctx.CatalogSet.SingleOrDefault(x => x.Id == expected.Id);
            Assert.That(actural.Name, Is.EqualTo(name));
            Assert.That(actural.Price, Is.EqualTo(price));

        }
    }

    public class CatalogSqlServerTest : SqlServerDbBaseTest
    {
        private TestDbContext _ctx;

        [SetUp]
        public void Setup()
        {
            this._ctx = _serviceProvider.GetService(typeof(TestDbContext)) as TestDbContext;
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
        }

        [TestCase("Oppo A96", 399.55)]
        public void test_create(string name, decimal price)
        {
            var expected = new Catalog()
            {
                Name = name,
                Price = price,
                CreatedBy = "TestA",
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