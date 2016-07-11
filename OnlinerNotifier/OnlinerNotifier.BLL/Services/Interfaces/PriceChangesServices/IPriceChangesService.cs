using OnlinerNotifier.BLL.Models.OnlinerDataModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Interfaces.PriceChangesServices
{
    public interface IPriceChangesService
    {
        void CompareAndUpdate(Product product, ProductOnliner newProduct);
    }
}