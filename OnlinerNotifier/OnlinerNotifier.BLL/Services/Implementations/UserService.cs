using System.Linq;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private UnitOfWork unitOfWork;
        private UserMapper userMapper;

        public UserService(UnitOfWork unitOfWork, UserMapper userMapper)
        {
            this.unitOfWork = unitOfWork;
            this.userMapper = userMapper;
        }

        public UserViewModel Get(int id)
        {
            var user = unitOfWork.Users.Get(id);
            if (user == null)
            {
                return null;
            }
            return userMapper.ToModel(user);
        }

        public int AddOrUpdate(OAuth2.Models.UserInfo userInfo)
        {
            var users = unitOfWork.Users;
            var user = users.GetAll().Where(usr => usr.ProviderName == userInfo.ProviderName)
                .FirstOrDefault(usr => usr.SocialId == userInfo.Id);
            if (user == null)
            {
                user = userMapper.ToDomain(userInfo);
                users.Create(user);
                unitOfWork.Save();
            }
            return user.Id;
        }
    }
}
