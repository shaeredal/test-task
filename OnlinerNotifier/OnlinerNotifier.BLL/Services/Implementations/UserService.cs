using System.Collections.Generic;
using System.Linq;
using OnlinerNotifier.BLL.Mappers.Implementations;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class UserService : IUserService, INotifiableUsersProvider
    {
        private IUnitOfWork unitOfWork;
        private UserMapper userMapper;
        private IEmailValidator emailValidator;

        public UserService(IUnitOfWork unitOfWork, UserMapper userMapper, IEmailValidator emailValidator)
        {
            this.unitOfWork = unitOfWork;
            this.userMapper = userMapper;
            this.emailValidator = emailValidator;
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

        public UserDataViewModel GetUserData(int id)
        {
            var user = unitOfWork.Users.Get(id);
            if (user == null)
            {
                return null;
            }
            return userMapper.ToDataModel(user);
        }

        public bool SetNotificationParameters(int userId, NotificationParametersModel parameters)
        {
            var user = unitOfWork.Users.Get(userId);
            if (user != null && emailValidator.IsValid(parameters.Email))
            {
                user.NotificationTime = parameters.Time;
                user.Email = parameters.Email;
                user.EnableNotifications = true;
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public bool DisableNotifications(int userId)
        {
            var user = unitOfWork.Users.Get(userId);
            if (user != null)
            {
                user.EnableNotifications = false;
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public List<User> GetNotifiableUsers()
        {
            return unitOfWork.Users.GetAllDeep()
                .Where(u => u.EnableNotifications)
                .Where(u => emailValidator.IsValid(u.Email))
                .ToList();
        }
    }
}
