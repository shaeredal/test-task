using System;
using System.Collections.Generic;
using System.Linq;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private IUnitOfWork unitOfWork;

        private EmailValidator emailValidator;

        public NotificationService(IUnitOfWork unitOfWork, EmailValidator emailValidator)
        {
            this.unitOfWork = unitOfWork;
            this.emailValidator = emailValidator;
        }

        public List<NotificationDataModel> GetNotificationData()
        {
            var users = unitOfWork.Users.GetAllDeep()
                .Where(u => u.EnableNotifications)
                .Where(u => emailValidator.IsValid(u.Email))
                .ToList();

            var result = new List<NotificationDataModel>();
            foreach (var user in users)
            {
                var userData = GetUserNotifications(user);
                if (userData.Products.Any())
                {
                    result.Add(userData);
                }
            }
            return result;
        }

        private NotificationDataModel GetUserNotifications(User user)
        {
            var result = new NotificationDataModel();
            result.User = user;
            foreach (var product in user.UserProducts.Where(up => up.IsTracked).Select(up => up.Product))
            {
                var changes = GetChanges(product);
                if (changes.Any())
                {
                    result.Products.Add(new NotificationProductChangesModel()
                    {
                        Product = product,
                        Changes = changes
                    });
                }
            }
            return result;
        }

        private List<ProductPriceChange> GetChanges(Product product)
        {
            return product.PriceChanges.Where(prod => DateTime.Now - prod.CheckTime < TimeSpan.FromHours(24)).ToList();
        }
    }
}
