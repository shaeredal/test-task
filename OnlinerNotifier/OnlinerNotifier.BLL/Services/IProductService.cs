using System.Collections.Generic;
using OnlinerNotifier.BLL.Models;

namespace OnlinerNotifier.BLL.Services
{
    public interface IProductService
    {
        void Add(ProductViewModel product, int userId);
        List<ProductViewModel> GetUserProducts(int userId);
    }
}
