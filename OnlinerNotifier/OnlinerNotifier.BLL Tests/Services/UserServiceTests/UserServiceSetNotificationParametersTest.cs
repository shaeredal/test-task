using System;
using NUnit.Framework;
using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL_Tests.Services.UserServiceTests
{
    public class UserServiceSetNotificationParametersTest : UserServiceTestBase
    {
        [TestCase(1, ExpectedResult = true)]
        [TestCase(11, ExpectedResult = false)]
        public bool SetNotificationParameters_ReturnValue_IsCorrect(int userId)
        {
            var notificationParameters = new NotificationParametersModel()
            {
                Email = "OnlinerNotifier@gmail.com",
                Time = new DateTime()
            };

            var result = userService.SetNotificationParameters(userId, notificationParameters);

            return result;
        }
    }
}
