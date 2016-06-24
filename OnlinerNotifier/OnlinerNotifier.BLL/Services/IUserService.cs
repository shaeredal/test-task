using System;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL.Services
{
    public interface IUserService
    {
        UserViewModel Get(int id);
        int AddOrUpdate(OAuth2.Models.UserInfo userInfo);
        UserDataViewModel GetUserData(int id);
        bool SetNotificationParameters(int userId, NotificationParametersModel parameters);
    }
}