using OAuth2.Models;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers
{
    public class UserMapper
    {
        public User GetFormUserInfo(UserInfo userInfo)
        {
            return new User()
            {
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                AvatarUri = userInfo.AvatarUri.Normal,
                Email = userInfo.Email,
                SocialId = userInfo.Id,
                ProviderName = userInfo.ProviderName
            };
        }

        public UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AvatarUri = user.AvatarUri
            };
        }
    }
}
