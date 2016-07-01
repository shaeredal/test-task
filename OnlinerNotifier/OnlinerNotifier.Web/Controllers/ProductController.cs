using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.Filters;
using OnlinerNotifier.Hubs;
using OnlinerNotifier.ToastNotifier;

namespace OnlinerNotifier.Controllers
{
    [Authentication]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService productService;
        private readonly IToastNotifier toastNotifer;

        public ProductController(IProductService productService, IToastNotifier toastNotifer)
        {
            this.productService = productService;
            this.toastNotifer = toastNotifer;
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
                toastNotifer.Send(product.Name + " is added.");
                return Ok();
            }
            return Conflict();
        }
    }
}
