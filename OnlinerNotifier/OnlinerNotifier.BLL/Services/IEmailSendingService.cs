using System.Collections.Generic;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services
{
    public interface IEmailSendingService
    {
        void SendChanges(User user, List<NotificationProductChangesModel> priceChanges);
    }
}
