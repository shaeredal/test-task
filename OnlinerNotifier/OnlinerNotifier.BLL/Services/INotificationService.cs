using System.Collections.Generic;
using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL.Services
{
    public interface INotificationService
    {
        List<NotificationDataModel> GetNotificationData();
    }
}
