using System;

namespace OnlinerNotifier.BLL.Services
{
    public interface INotificationTimeCalculationService
    {
        DateTime Calculate(DateTime userNotificationTimeUtc);
    }
}
