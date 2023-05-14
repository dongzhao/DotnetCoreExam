
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitTest
{
    public class CatalogRepositoryInMemoryTest : InMemoryDbBaseTest
    {
        private IUnitOfWork _unitOfWork ;

        [SetUp]
        public void Setup()
        {
            this._unitOfWork = _serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
        }

        [Test, Order(1)]
        public async Task test_add_async()
        {
            var expected = new Catalog()
            {
                Name = "Oppo A96",
                Price = (decimal)399.55,
                CreatedDateTime = DateTime.Now.Date,
                CreatedBy = "TestA"
            };
            await _unitOfWork.CatalogRepository.AddAsync(expected);
            await _unitOfWork.CommitChangesAsync();

            Assert.IsTrue(expected.Id > 0);
        }

        [Test, Order(2)]
        public async Task test_get_async()
        {
            var expected = new Catalog()
            {
                Id = 1,
                Name = "Oppo A96",
                Price = (decimal)399.55,
                CreatedDateTime = DateTime.Now.Date,
                CreatedBy = "TestA"
            };

            var actural = await _unitOfWork.CatalogRepository.Get(expected.Id);
            Assert.IsNotNull(actural);
            // verify all fields
        }

        [Test, Order(3)]
        public async Task test_modify_async()
        {
            var expected = new Catalog()
            {
                Id = 1,
                Name = "IPhone 12",
                Price = (decimal)1111.55,
                CreatedDateTime = DateTime.Now.AddDays(1),
                CreatedBy = "TestB"
            };

            var actural = await _unitOfWork.CatalogRepository.Get(expected.Id);

            // update fields
            await _unitOfWork.CatalogRepository.UpdateAsync(actural);
            await _unitOfWork.CommitChangesAsync();

            Assert.IsNotNull(actural);
            // verify all fields
        }

        [Test, Order(4)]
        public async Task test_delete_async()
        {
            var expected = new Catalog()
            {
                Id = 1,
            };

            var actural = await _unitOfWork.CatalogRepository.de(expected.Id);

            // update fields
            await _unitOfWork.CatalogRepository.UpdateAsync(actural);
            await _unitOfWork.CommitChangesAsync();

            Assert.IsNotNull(actural);
            // verify all fields
        }
    }

}