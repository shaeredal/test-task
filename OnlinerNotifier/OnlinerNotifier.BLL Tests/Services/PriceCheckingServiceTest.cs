using System.Linq;
using Moq;
using NUnit.Framework;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL_Tests.Moq;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL_Tests.Services
{
    public class PriceCheckingServiceTest
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private IPricesCheckingService pricesCheckingService;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            unitOfWorkMock = mockStorage.UnitOfWorkMock;
            var onlinerSearchServiceMock = mockStorage.OnlinerSearchServiceMock;
            unitOfWorkMock.Setup(m => m.PriceCanges.Create(It.IsAny<ProductPriceChange>()));

            pricesCheckingService = new PricesCheckingService(unitOfWorkMock.Object, new PriceChangesMapper(), onlinerSearchServiceMock.Object);
        }

        [Test]
        public void Check_Checking_ChecksProducts()
        {
            var testProduct = unitOfWorkMock.Object.Products.GetAll().FirstOrDefault(p => p.OnlinerId == 12345);

            pricesCheckingService.Check();

            unitOfWorkMock.Verify(m => m.PriceCanges.Create(It.IsAny<ProductPriceChange>()), Times.Once);
            Assert.AreEqual(70000, testProduct.MaxPrice);
            Assert.AreEqual(50000, testProduct.MinPrice);
        }
    }
}
