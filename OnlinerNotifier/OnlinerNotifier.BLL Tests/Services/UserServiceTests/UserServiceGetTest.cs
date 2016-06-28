using System;
using NUnit.Framework;

namespace OnlinerNotifier.BLL_Tests.Services.UserServiceTests
{
    public class UserServiceGetTest : UserServiceTestBase
    {
        [TestCase(1, ExpectedResult = "TestName")]
        public string Get_GetUserById_GetUserViewModelWithSpecificName(int id)
        {
            var result = userService.Get(id).FirstName;

            return result;
        }

        [Test]
        public void Get_GetUserById_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(delegate { var result = userService.Get(2).FirstName; });
        }
    }
}
