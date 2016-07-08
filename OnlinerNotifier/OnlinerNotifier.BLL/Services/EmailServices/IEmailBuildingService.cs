using System.Collections.Generic;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.EmailServices
{
    public interface IEmailBuildingService
    {
        NotificationEmailModel GetEmail(User user, List<NotificationProductChangesModel> priceChanges);
    }
}
