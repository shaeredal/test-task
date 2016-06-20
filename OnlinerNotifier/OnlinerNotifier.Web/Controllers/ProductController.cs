using System.Linq;
using System.Net.Http;
using System.Web.Http;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services;

namespace OnlinerNotifier.Controllers
{
    public class ProductController : ApiController
    {
        private IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public void Post(ProductViewModel product)
        {
            var userIdCookie = Request.Headers.GetCookies("User").FirstOrDefault();
            if (userIdCookie != null)
            {
                int userId;
                if (int.TryParse(userIdCookie["User"].Value, out userId))
                {
                    productService.Add(product, userId);
                }
            }
        }
    }
}
