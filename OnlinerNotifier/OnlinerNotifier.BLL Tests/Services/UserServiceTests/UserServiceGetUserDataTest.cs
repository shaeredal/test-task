using NUnit.Framework;

namespace OnlinerNotifier.BLL_Tests.Services.UserServiceTests
{
    public class UserServiceGetUserDataTest : UserServiceTestBase
    {
        [Test]
        public void GetUserData_UserData_GetUserData()
        {
            var result = userService.GetUserData(1);

            Assert.AreEqual("TestName", result.FirstName);
            Assert.AreEqual("TestLastName", result.LastName);
        }

        [Test]
        public void GetUserData_UserData_GetNull()
        {
            var result = userService.GetUserData(11);

            Assert.IsNull(result);
        }
    }
}
