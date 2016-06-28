using System;
using Moq;
using NUnit.Framework;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL_Tests.Moq;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.BLL_Tests.Services
{
    [TestFixture]
    public class UserServiceGetTest
    {
        private Mock<IUnitOfWork> unitOfWorkMock;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            unitOfWorkMock = mockStorage.GetUnitOfWorkMock;
        }

        [TestCase(1, ExpectedResult = "TestName")]
        public string Get_GetUserById_GetUserViewModelWithSpecificName(int id)
        {
            var userService = new UserService(unitOfWorkMock.Object, new UserMapper(new UserProductsMapper(new ProductMapper())));

            var result = userService.Get(id).FirstName;

            return result;
        }

        [Test]
        public void Get_GetUserById_ThrowsException()
        {
            var userService = new UserService(unitOfWorkMock.Object, new UserMapper(new UserProductsMapper(new ProductMapper())));

            Assert.Throws<NullReferenceException>(delegate { var result = userService.Get(2).FirstName; });
        }
    }
}
