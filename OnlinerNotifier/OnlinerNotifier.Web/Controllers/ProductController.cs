using System.Collections.Generic;
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

        public void Post(ProductViewModel product)
        {
            var userId = Principal.Id;
            productService.Add(product, userId);
        }
    }
}
