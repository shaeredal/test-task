using System.Collections.Generic;
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
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService productService;
        private readonly IToastNotifier toastNotifier;

        public ProductController(IProductService productService, IToastNotifier toastNotifier)
        {
            this.productService = productService;
            this.toastNotifier = toastNotifier;
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
                SendNotification(product.Name, "is added.");
                return Ok();
            }
            return Conflict();
        }

        public IHttpActionResult Delete(int id)
        {
            if (productService.Delete(Principal.Id, id))
            {
                SendNotification("Product", "is deleted."); //TODO: get product name
                return Ok();
            }
            return NotFound();
        }

        private void SendNotification(string productName, string message)
        {
            toastNotifier.Send(Request, $"{productName} {message}");
        }
    }
}
