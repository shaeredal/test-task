using System;
using System.Collections.Generic;
using System.Linq;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class NotificationDataDataService : INotificationDataService
    {
        private INotifiableUsersProvider notifiableUsersProvider;

        private INotificationProductChangesModelMapper notificationProductChangesModelMapper;

        private INotificationDataModelMapper notificationDataModelMapper;

        public NotificationDataDataService(INotifiableUsersProvider notifiableUsersProvider,
            INotificationProductChangesModelMapper notificationProductChangesModelMapper,
            INotificationDataModelMapper notificationDataModelMapper)
        {
            this.notifiableUsersProvider = notifiableUsersProvider;
            this.notificationProductChangesModelMapper = notificationProductChangesModelMapper;
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
            return notificationDataModelMapper.ToModel(user, GetProductChanges(user, period));
        }

        private List<NotificationProductChangesModel> GetProductChanges(User user, TimeSpan period)
        {
            var result = new List<NotificationProductChangesModel>();
            foreach (var product in GetTrackedProducts(user))
            {
                var changes = GetProductPriceChanges(product, period);
                if (changes.Any())
                {
                    result.Add(notificationProductChangesModelMapper.ToModel(product, changes));
                }
            }
            return result;
        }

        private List<Product> GetTrackedProducts(User user)
        {
            return user.UserProducts.Where(up => up.IsTracked).Select(up => up.Product).ToList();
        }

        private List<ProductPriceChange> GetProductPriceChanges(Product product, TimeSpan period)
        {
            return product.PriceChanges.Where(prod => DateTime.Now - prod.CheckTime < period).ToList();
        }
    }
}
