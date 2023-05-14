
using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Core.UnitTest
{
    public class OrderItemInMemoryTest : InMemoryDbBaseTest
    {

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

    public class OrderItemSqlServerTest : SqlServerDbBaseTest
    {

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