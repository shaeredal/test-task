using System.Collections.Generic;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services
{
    public interface IEmailService
    {
        void SendChanges(User user, List<NotificationProductChangesModel> priceChanges);

        void SendChanges(NotificationEmailModel emailModel);

        NotificationEmailModel GetEmail(User user, List<NotificationProductChangesModel> priceChanges);
    }
}
