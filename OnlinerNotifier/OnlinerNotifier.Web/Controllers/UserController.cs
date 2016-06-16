using System.Web.Http;
using OnlinerNotifier.DAL;
using OnlinerNotifier.Models;

namespace OnlinerNotifier.Controllers
{
    public class UserController : ApiController
    {
        private UnitOfWork unitOfWork;

        public UserController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UserViewModel Get(int id)
        {
            var user = unitOfWork.Users.Get(id);

            if (user == null)
            {
                return null;
            }

            return new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AvatarUri = user.AvatarUri
            };
        } 
    }
}
