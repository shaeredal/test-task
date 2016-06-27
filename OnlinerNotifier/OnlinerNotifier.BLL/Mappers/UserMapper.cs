using System.Linq;
using OAuth2.Models;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers
{
    public class UserMapper
    {
        private UserProductsMapper userProductsMapper;

        public UserMapper(UserProductsMapper userProductsMapper)
        {
            this.userProductsMapper = userProductsMapper;
        }

        public User ToDomain(UserInfo userInfo)
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

        public UserViewModel ToModel(User user)
        {
            return new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AvatarUri = user.AvatarUri
            };
        }

        public UserDataViewModel ToDataModel(User user)
        {
            return new UserDataViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AvatarUri = user.AvatarUri,
                Email = user.Email,
                UserProducts = user.UserProducts.Select(up => userProductsMapper.ToModel(up)).ToList()
            };
        }
    }
}
