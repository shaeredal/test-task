using OnlinerNotifier.BLL.Models;

namespace OnlinerNotifier.BLL.Services
{
    public interface IUserService
    {
        UserViewModel Get(int id);
        int AddOrUpdate(OAuth2.Models.UserInfo userInfo);
    }
}