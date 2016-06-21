using System.Web.Http;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.Filters;

namespace OnlinerNotifier.Controllers
{
    [Authentication]
    public class AccountController : ApiControllerBase
    {
        private readonly IUserService userService;
        private readonly IProductService productService;

        public AccountController(IUserService userService, IProductService productService)
        {
            this.userService = userService;
            this.productService = productService;
        }

        public UserDataViewModel GetData()
        {
            return userService.GetUserData(Principal.Id);
        }

        public IHttpActionResult Delete(int id)
        {
            if (productService.Delete(Principal.Id, id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
