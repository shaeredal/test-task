using System;
using System.Collections.Generic;
using System.Linq;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.BLL.Services.Interfaces.NotificationDataSevices;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations.NotificationDataSevices
{
    public class NotificationDataDataService : INotificationDataService
    {
        private INotifiableUsersProvider notifiableUsersProvider;

        private IUserProductChangesService userProductChangesService;

        private INotificationDataModelMapper notificationDataModelMapper;

        public NotificationDataDataService(INotifiableUsersProvider notifiableUsersProvider,
            IUserProductChangesService userProductChangesService,
            INotificationDataModelMapper notificationDataModelMapper)
        {
            this.notifiableUsersProvider = notifiableUsersProvider;
            this.userProductChangesService = userProductChangesService;
            this.notificationDataModelMapper = notificationDataModelMapper;
        }

        public List<NotificationDataModel> CollectNotificationData(TimeSpan period)
        {
            var users = notifiableUsersProvider.GetNotifiableUsers();
            var result = new List<NotificationDataModel>();
            foreach (var user in users)
            {
                var userData = GetUserNotifications(user, period);
                if (userData.Products.Any())
                {
                    result.Add(userData);
                }
            }
            return result;
        }

        private NotificationDataModel GetUserNotifications(User user, TimeSpan period)
        {
            return notificationDataModelMapper.ToModel(user, userProductChangesService.GetProductChanges(user, period));
        }
    }
}
