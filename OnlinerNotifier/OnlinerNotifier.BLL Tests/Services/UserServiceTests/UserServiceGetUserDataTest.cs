using NUnit.Framework;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Services.Implementations;

namespace OnlinerNotifier.BLL_Tests.Services.UserServiceTests
{
    public class UserServiceGetUserDataTest : UserServiceTestBase
    {
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
