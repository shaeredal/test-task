using System;
using NUnit.Framework;
using OnlinerNotifier.BLL.Services.Implementations;

namespace OnlinerNotifier.BLL_Tests.Services
{
    [TestFixture]
    public class TimeCalculationServiceTest
    {
        [Test]
        public void CalculateNotificationTime_TimeOfDay_IsThreeHoursMore()
        {
            var userNotificationTime = new DateTime(1970, 1, 1, 11, 30, 0);
            var timeCalculationService = new TimeCalculationService();
            var result = timeCalculationService.CalculateNotificationTime(userNotificationTime);
            var expectedResult = userNotificationTime + TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);

            Assert.AreEqual(expectedResult.TimeOfDay, result.TimeOfDay);
        }

        [Test]
        public void CalculateNotificationTimeTest_TimeToNotification_LessThanADay()
        {
            var userNotificationTime = new DateTime(1970, 1, 1, 11, 30, 0);
            var timeCalculationService = new TimeCalculationService();
            var result = timeCalculationService.CalculateNotificationTime(userNotificationTime);

            Assert.IsTrue(result - DateTime.Now <= TimeSpan.FromDays(1));
        }
    }
}
