using System.Linq;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private UnitOfWork unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int AddOrUpdate(OAuth2.Models.UserInfo userInfo)
        {
            var users = unitOfWork.Users;
            var user = users.GetAll().Where(usr => usr.ProviderName == userInfo.ProviderName)
                .FirstOrDefault(usr => usr.SocialId == userInfo.Id);
            if (user == null)
            {
                user = new User()
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    AvatarUri = userInfo.AvatarUri.Normal,
                    Email = userInfo.Email,
                    SocialId = userInfo.Id,
                    ProviderName = userInfo.ProviderName
                };
                users.Create(user);
                unitOfWork.Save();
            }
            return user.Id;
        }
    }
}
