using System.Web.Http;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.Filters;

namespace OnlinerNotifier.Controllers
{
    [Authentication]
    public class ProductController : ApiController
    {
        private IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public void Post(ProductViewModel product)
        {
            var userId = ((Principal) RequestContext.Principal).Id;
            productService.Add(product, userId);
        }
    }
}
