namespace OnlinerNotifier.BLL.Services
{
    public interface IUserService
    {
        int AddOrUpdate(OAuth2.Models.UserInfo userInfo);
    }
}