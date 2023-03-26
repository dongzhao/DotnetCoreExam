using DDD.Model.DomainEvents;
using DDD.Model.Entities;
using DDD.Model.Enums;
using DDD.Model.Values;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDD.Model.UnitTest
{
    public class OrderInMemoryTest : InMemoryBaseTest
    {
        private ShoppingCartDbContext _ctx ;

        [SetUp]
        public void Setup()
        {
            this._ctx = _serviceProvider.GetService(typeof(ShoppingCartDbContext)) as ShoppingCartDbContext;
        }

        [Test]
        public void Test1()
        {

        }

        [TestCase(
            new string[] { "101 dummy street", "Sydney", "NSW", "2000", "AUS" } ,
            new string[] { "102 dummy street", "Sydney", "NSW", "2001", "AUS" } ,
            new string[] { "TEST", "100000000001", "2", "22", "101" },
            new string[] { "Eric", "Johnson", "abc@test.com", "0101100101"},
            new string[] { "2022-12-6", "MasterCard", "Pending" }
        )]
        public void test_create(string[] shippingAddress, string[] residentalAddress, string[] cardDetails, string[] recipentDetails, string[] orderDetails)
        {
            var address1 = new Address() 
            { 
                Street = shippingAddress[0],
                City = shippingAddress[1],
                State = shippingAddress[2],
                PostalCode = shippingAddress[3],
                Country = shippingAddress[4],
            };

            var address2 = new Address()
            {
                Street = residentalAddress[0],
                City = residentalAddress[1],
                State = residentalAddress[2],
                PostalCode = residentalAddress[3],
                Country = residentalAddress[4],
            };

            var creditcard = new CreditCard()
            {
                CardHolderName = cardDetails[0],
                CardNumber = cardDetails[1],
                ExpireMonth = Convert.ToInt32(cardDetails[2]),
                ExpireYear = Convert.ToInt32(cardDetails[3]),
                CVSNumber = cardDetails[4]
            };

            var recipient = new Recipient()
            {
                FirstName = recipentDetails[0],
                LastName = recipentDetails[1],
                Email = recipentDetails[2],
                MobileNumber = recipentDetails[3]
            };

            var expected = new Order()
            {
                ShippingAddress = address1,
                ResidentAddress = address2,
                CurrentDateTime = DateTime.Parse(orderDetails[0]),
                CardType = (CardType)Enum.Parse(typeof(CardType), orderDetails[1]), //CardType.MasterCard,
                Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderDetails[2]), //OrderStatus.Pending,
                CreditCard = creditcard,
                Recipient = recipient,
            };

            _ctx.OrderSet.Add(expected);
            _ctx.SaveChanges();
            Assert.IsTrue(expected.Id > 0);

            var actual = _ctx.OrderSet.SingleOrDefault(e => e.Id == expected.Id);
            Assert.IsNotNull(actual);
            Assert.Multiple(() =>
            {
                Assert.That(actual.CurrentDateTime, Is.EqualTo(DateTime.Parse(orderDetails[0])));
                Assert.That(actual.CardType, Is.EqualTo((CardType)Enum.Parse(typeof(CardType), orderDetails[1])));
                Assert.That(actual.Status, Is.EqualTo((OrderStatus)Enum.Parse(typeof(OrderStatus), orderDetails[2])));
            });
            
        }
    }

    public class OrderSqlServerTest : SqlServerBaseTest
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
        public void test_create()
        {
            var catalog = new Catalog()
            {
                Name = "Oppo A96",
                Price = (decimal)399.55,
            };

            var address1 = new Address()
            {
                Street = "101 dummy street",
                City = "Sydney",
                State = "NSW",
                PostalCode = "2000",
                Country = "AUS",
            };

            var address2 = new Address()
            {
                Street = "102 dummy street",
                City = "Sydney",
                State = "NSW",
                PostalCode = "2001",
                Country = "AUS",
            };

            var creditcard = new CreditCard()
            {
                CardHolderName = "TEST",
                CardNumber = "100000000001",
                ExpireMonth = 2,
                ExpireYear = 22,
                CVSNumber = "101"
            };

            var recipient = new Recipient()
            {
                FirstName = "Eric",
                LastName = "Johnson",
                Email = "abc@test.com",
                MobileNumber = "0101100101"
            };
            var expected = new Order()
            {
                ShippingAddress = address1,
                ResidentAddress = address2,
                CurrentDateTime = DateTime.Now,
                CardType = CardType.MasterCard,
                Status = OrderStatus.Pending,
                CreditCard = creditcard,
                Recipient = recipient,

            };

            _ctx.OrderSet.Add(expected);
            _ctx.SaveChanges();
            Assert.IsTrue(expected.Id > 0);
        }
    }

    public class OrderEventTest : DomainEventBaseTest
    {
        private ShoppingCartDbContext _ctx;
        private IMediator _mediator;

        [SetUp]
        public void Setup()
        {
            this._ctx = _serviceProvider.GetService(typeof(ShoppingCartDbContext)) as ShoppingCartDbContext;
            this._mediator = _serviceProvider.GetService(typeof(IMediator)) as IMediator;
        }

        [TestCase(
            new string[] { "2022-12-6", "MasterCard", "Pending" },
            ""
        )]
        public void test_create(string[] orderDetails, string expected)
        {
            var orderObj = new Order()
            {
                CurrentDateTime = DateTime.Parse(orderDetails[0]),
                CardType = (CardType)Enum.Parse(typeof(CardType), orderDetails[1]), //CardType.MasterCard,
                Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderDetails[2]), //OrderStatus.Pending,
            };
            _ctx.OrderSet.Add(orderObj);
            var orderConfirmedEvent = new OrderConfirmedEvent(orderObj);
            orderObj.AddDomainEvent(orderConfirmedEvent);

            _ctx.SaveChanges();
            Assert.IsTrue(orderObj.Id > 0);

            _mediator.Publish(orderConfirmedEvent);
            orderObj.RemoveDomainEvent(orderConfirmedEvent);
                

            var actual = _ctx.OrderSet.SingleOrDefault(e => e.Id == orderObj.Id);
            Assert.IsNotNull(actual);

        }
    }

}