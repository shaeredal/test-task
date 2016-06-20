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

        public void Post(ProductViewModel product, int userId)
        {
            
        }
    }
}
