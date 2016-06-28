using Moq;
using NUnit.Framework;
using OAuth2.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL_Tests.Services.UserServiceTests
{
    public class UserServiceAddOrUpdateTest : UserServiceTestBase
    {
        [Test]
        public void AddOrUpdate_CreateNewUser_IsCreates()
        {
            var userInfo = new UserInfo() {Id = "42"};

            userService.AddOrUpdate(userInfo);

            userRepositoryMock.Verify(ur =>ur.Create(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void AddOrUpdate_CreateNewUser_ReturnsId()
        {
            var userInfo = new UserInfo() { Id="not 42" };

            var result = userService.AddOrUpdate(userInfo);

            Assert.AreEqual(42, result);
        }
    }
}
