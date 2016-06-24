using System.Linq;
using OAuth2.Models;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers
{
    public class UserMapper
    {
        private ProductMapper productMapper;

        public UserMapper(ProductMapper productMapper)
        {
            this.productMapper = productMapper;
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
                Products = user.UserProducts.Select(up => up.Product).Select(prod => productMapper.ToModel(prod)).ToList()
            };
        }
    }
}
