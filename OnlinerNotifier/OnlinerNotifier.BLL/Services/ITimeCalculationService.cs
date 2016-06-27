using System;

namespace OnlinerNotifier.BLL.Services
{
    public interface ITimeCalculationService
    {
        DateTime CalculateNotificationTime(DateTime userNotificationTimeUtc);
    }
}
