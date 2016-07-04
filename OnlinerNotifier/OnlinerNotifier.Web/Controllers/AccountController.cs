using System.Linq;
using System.Net.Http;
using System.Web.Http;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.Filters;
using OnlinerNotifier.ToastNotifier;

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
