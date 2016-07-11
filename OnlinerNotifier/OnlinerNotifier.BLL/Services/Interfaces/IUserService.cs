using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL.Services.Interfaces
{
    public interface IUserService
    {
        UserViewModel Get(int id);
        int AddOrUpdate(OAuth2.Models.UserInfo userInfo);
        UserDataViewModel GetUserData(int id);
        bool SetNotificationParameters(int userId, NotificationParametersModel parameters);
        bool DisableNotifications(int userId);
    }
}