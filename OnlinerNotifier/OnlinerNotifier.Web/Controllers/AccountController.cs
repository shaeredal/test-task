using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.Filters;

namespace OnlinerNotifier.Controllers
{
    [Authentication]
    public class AccountController : ApiControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        public UserDataViewModel GetData()
        {
            return userService.GetUserData(Principal.Id);
        }
    }
}
