using DDD.Model.Entities;
using DDD.Model.Enums;
using DDD.Model.Values;
using Microsoft.EntityFrameworkCore;

namespace DDD.Model.UnitTest
{
    public class OrderItemInMemoryTest : InMemoryBaseTest
    {
        private ShoppingCartDbContext _ctx ;

        [SetUp]
        public void Setup()
        {
            this._ctx = _serviceProvider.GetService(typeof(ShoppingCartDbContext)) as ShoppingCartDbContext;
        }

        [TestCase(
            new string[] { "Oppo A96", "399.55" }, 
            new string[] { "Pending", "2022-12-6" },
            new string[] { "5"}
        )]
        public void test_create(string[] catalogData, string[] orderData, string[] orderItem)
        {
            var catalog = new Catalog()
            {
                Name = catalogData[0],
                Price = Convert.ToDecimal(catalogData[1]),
            };

            var order = new Order()
            {
                Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderData[0]),
                CurrentDateTime = DateTime.Parse(orderData[1]),
            };

            var expected = new OrderItem()
            {
                Catalog = catalog,
                Order = order,
                Quantity = Convert.ToInt32(orderItem[0]),               
            };

            order.OrderItems.Add(expected);

            _ctx.OrderItemSet.Add(expected);
            _ctx.SaveChanges();
            Assert.IsTrue(expected.Id > 0);

            var actural = _ctx.OrderItemSet.SingleOrDefault(x => x.Id == expected.Id);
            Assert.IsNotNull(actural);
            Assert.Multiple(()=>
            {
                Assert.That(actural.Quantity, Is.EqualTo(Convert.ToInt32(orderItem[0])));
                Assert.That(actural.Catalog.Name, Is.EqualTo(catalogData[0]));
                Assert.That(actural.Catalog.Price, Is.EqualTo(Convert.ToDecimal(catalogData[1])));
                Assert.That(actural.Order.Status, Is.EqualTo((OrderStatus)Enum.Parse(typeof(OrderStatus), orderData[0])));
                Assert.That(actural.Order.CurrentDateTime, Is.EqualTo(DateTime.Parse(orderData[1])));
            });
           
        }
    }

    public class OrderItemSqlServerTest : SqlServerBaseTest
    {
        private ShoppingCartDbContext _ctx;

        [SetUp]
        public void Setup()
        {
            this._ctx = _serviceProvider.GetService(typeof(ShoppingCartDbContext)) as ShoppingCartDbContext;
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
        }

        [TestCase(new string[] { "Oppo A96", "399.55" },
            new string[] { "Pending", "2022-12-6" },
            new string[] { "5" }
        )]
        public void test_create(string[] catalogData, string[] orderData, string[] orderItem)
        {
            var catalog = new Catalog()
            {
                Name = catalogData[0],
                Price = Convert.ToDecimal(catalogData[1]),
            };

            var order = new Order()
            {
                Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderData[0]),
                CurrentDateTime = DateTime.Parse(orderData[1]),
            };

            var expected = new OrderItem()
            {
                Catalog = catalog,
                Order = order,
                Quantity = Convert.ToInt32(orderItem[0]),
            };

            order.OrderItems.Add(expected);

            _ctx.OrderItemSet.Add(expected);
            _ctx.SaveChanges();
            Assert.IsTrue(expected.Id > 0);

            var actural = _ctx.OrderItemSet.SingleOrDefault(x => x.Id == expected.Id);
            Assert.IsNotNull(actural);
            Assert.Multiple(() =>
            {
                Assert.That(actural.Quantity, Is.EqualTo(Convert.ToInt32(orderItem[0])));
                Assert.That(actural.Catalog.Name, Is.EqualTo(catalogData[0]));
                Assert.That(actural.Catalog.Price, Is.EqualTo(Convert.ToDecimal(catalogData[1])));
                Assert.That(actural.Order.Status, Is.EqualTo((OrderStatus)Enum.Parse(typeof(OrderStatus), orderData[0])));
                Assert.That(actural.Order.CurrentDateTime, Is.EqualTo(DateTime.Parse(orderData[1])));
            });

        }
    }

}