using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using OAuth2.Models;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL_Tests.Moq;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.BLL_Tests.Services
{
    [TestFixture]
    public class UserServiceAddOrUpdateTest
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IUserRepository> userRepositoryMock;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            unitOfWorkMock = mockStorage.GetUnitOfWorkMock;
            userRepositoryMock = mockStorage.GetUserRepositoryMock;
        }

        [Test]
        public void AddOrUpdate_CreateNewUser_IsCreates()
        {
            var userService = new UserService(unitOfWorkMock.Object, new UserMapper(new UserProductsMapper(new ProductMapper())));
            var userInfo = new UserInfo() {Id = "42"};

            userService.AddOrUpdate(userInfo);

            userRepositoryMock.Verify(ur =>ur.Create(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void AddOrUpdate_CreateNewUser_ReturnsId()
        {
            var userService = new UserService(unitOfWorkMock.Object, new UserMapper(new UserProductsMapper(new ProductMapper())));
            var userInfo = new UserInfo() { Id="not 42" };

            var result = userService.AddOrUpdate(userInfo);

            Assert.AreEqual(42, result);
        }
    }
}
