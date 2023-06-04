
using Core.DomainEvents;
using Core.Entities;
using Core.Enums;
using Core.Values;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.UnitTest
{
    public class OrderRepositoryInMemoryTest : DomainEventBaseTest
    {
        [TestCase(
            new string[] { "101 dummy street", "Sydney", "NSW", "2000", "AUS" },
            new string[] { "102 dummy street", "Sydney", "NSW", "2001", "AUS" },
            new string[] { "TEST", "100000000001", "2", "22", "101" },
            new string[] { "Eric", "Johnson", "abc@test.com", "0101100101" },
            new string[] { "2022-12-6", "MasterCard", "Pending" }
        ), Order(1)]
        public async Task test_add_sync_with_notification(string[] shippingAddress, string[] residentalAddress, string[] cardDetails, string[] recipentDetails, string[] orderDetails)
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

            var expected1 = new Order()
            {
                ShippingAddress = address1,
                ResidentAddress = address2,
                CurrentDateTime = DateTime.Parse(orderDetails[0]),
                CardType = (CardType)Enum.Parse(typeof(CardType), orderDetails[1]), //CardType.MasterCard,
                Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderDetails[2]), //OrderStatus.Pending,
                CreditCard = creditcard,
                Recipient = recipient,
            };
            var actural1 = await _unitOfWork.OrderRepository.AddAsync(expected1);


            await _unitOfWork.CommitChangesAsync();

            Assert.IsTrue(actural1.Id > 0);

        }

    }

}