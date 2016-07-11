using NUnit.Framework;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.BLL_Tests.Moq;

namespace OnlinerNotifier.BLL_Tests.Services
{
    [TestFixture]
    public class NotificationServiceTest
    {
        private INotificationService notificationService;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            notificationService = new NotificationService(mockStorage.UnitOfWorkMock.Object, new EmailValidator());
        }

        [TestCase(0, ExpectedResult = 100)]
        [TestCase(1, ExpectedResult = 101)]
        public decimal GetNotificationData_Data_CorrectData(int index)
        {
            var data = notificationService.GetNotificationData();
            var result = data[0].Products[0].Changes[index].NewMinPrice;
            
            return result;
        }

        [Test]
        public void GetNotificationData_Data_OlderThanDayAreNotIncluded()
        {
            var data = notificationService.GetNotificationData();
            var result = data[0].Products[0].Changes;

            Assert.AreEqual(2, result.Count);
        }
    }
}
