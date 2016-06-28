using Moq;
using NUnit.Framework;
using OAuth2.Models;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL_Tests.Services.UserServiceTests
{
    public class UserServiceAddOrUpdateTest : UserServiceTestBase
    {
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
