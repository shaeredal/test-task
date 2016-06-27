using System;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class NotificationTimeCalculationService : INotificationTimeCalculationService
    {
        public DateTime Calculate(DateTime userNotificationTimeUtc)
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            var userNotificationTime = userNotificationTimeUtc + offset;
            var notificationTime = DateTime.Today + userNotificationTime.TimeOfDay;
            if (notificationTime.TimeOfDay < DateTime.Now.TimeOfDay)
            {
                notificationTime += TimeSpan.FromDays(1);
            }
            return notificationTime;
        }
    }
}
