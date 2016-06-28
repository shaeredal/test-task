using Moq;
using NUnit.Framework;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL_Tests.Moq;
using OnlinerNotifier.DAL;

namespace OnlinerNotifier.BLL_Tests.Services
{
    [TestFixture]
    public class UserServiceGetUserDataTest
    {
        private Mock<IUnitOfWork> unitOfWorkMock;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            unitOfWorkMock = mockStorage.UnitOfWorkMock;
        }

        [Test]
        public void GetUserData_UserData_GetUserData()
        {
            var userService = new UserService(unitOfWorkMock.Object, new UserMapper(new UserProductsMapper(new ProductMapper())));

            var result = userService.GetUserData(1);

            Assert.AreEqual("TestName", result.FirstName);
            Assert.AreEqual("TestLastName", result.LastName);
        }

        [Test]
        public void GetUserData_UserData_GetNull()
        {
            var userService = new UserService(unitOfWorkMock.Object, new UserMapper(new UserProductsMapper(new ProductMapper())));

            var result = userService.GetUserData(11);

            Assert.IsNull(result);
        }
    }
}
