using System;
using System.Collections.Generic;
using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL.Services.Interfaces
{
    public interface INotificationDataService
    {
        List<NotificationDataModel> CollectNotificationData(TimeSpan period);
    }
}
