using System.Collections.Generic;
using System.Web.Http;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.Filters;

namespace OnlinerNotifier.Controllers
{
    [Authentication]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public List<ProductViewModel> Get()
        {
            return productService.GetUserProducts(Principal.Id);
        }

        public IHttpActionResult Post(ProductViewModel product)
        {
            var userId = Principal.Id;
            if (productService.Add(product, userId))
            {
                return Ok();
            }
            return Conflict();
        }
    }
}
