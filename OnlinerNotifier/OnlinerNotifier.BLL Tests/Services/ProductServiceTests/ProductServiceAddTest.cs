using System.Linq;
using Moq;
using NUnit.Framework;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL_Tests.Services.ProductServiceTests
{
    public class ProductServiceAddTest : ProductServiceTestBase
    {
        [TestCase(12346, 1)]
        [TestCase(12345, 0)]
        public void Add_ProductAddition_Added(int onlinerId, int times)
        {
            var productViewModel = new ProductViewModel() { OnlinerId = onlinerId };

            productService.Add(productViewModel, 1);

            productRepositoryMock.Verify(p => p.Create(It.IsAny<Product>()), Times.Exactly(times));
            Assert.AreEqual(1, userMock.Object.UserProducts.Count());
        }

        [Test]
        public void Add_ProductAddition_RelationIsNotDuplicated()
        {
            var productViewModel = new ProductViewModel() { OnlinerId = 12345 };

            productService.Add(productViewModel, 1);
            productService.Add(productViewModel, 1);

            Assert.AreEqual(1, userMock.Object.UserProducts.Count());
        }
    }
}
