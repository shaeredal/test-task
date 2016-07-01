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
        private readonly IProductService productService;
        private readonly IToastNotifier toastNotifier;

        public AccountController(IUserService userService, IProductService productService, IToastNotifier toastNotifier)
        {
            this.userService = userService;
            this.productService = productService;
            this.toastNotifier = toastNotifier;
        }

        public UserDataViewModel GetData()
        {
            return userService.GetUserData(Principal.Id);
        }

        public IHttpActionResult Delete(int id)
        {
            if (productService.Delete(Principal.Id, id))
            {
                toastNotifier.Send("Product is deleted.");
                return Ok();
            }
            return NotFound();
        }
    }
}
