using System.Web.Http;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services;

namespace OnlinerNotifier.Controllers
{
    public class UserController : ApiController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public UserViewModel Get(int id)
        {
            return userService.Get(id);
        } 
    }
}
