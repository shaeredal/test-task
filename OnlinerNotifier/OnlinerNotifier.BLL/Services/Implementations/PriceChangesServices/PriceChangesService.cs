using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models.OnlinerDataModels;
using OnlinerNotifier.BLL.Services.Interfaces.PriceChangesServices;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations.PriceChangesServices
{
    public class PriceChangesService : IPriceChangesService
    {
        private IUnitOfWork unitOfWork;

        private IPriceChangesMapper priceChangesMapper;

        public PriceChangesService(IUnitOfWork unitOfWork, IPriceChangesMapper priceChangesMapper)
        {
            this.unitOfWork = unitOfWork;
            this.priceChangesMapper = priceChangesMapper;
        }

        public void CompareAndUpdate(int productId, PriceOnliner newPrice)
        {
            var product = unitOfWork.Products.Get(productId);
            if (newPrice == null)
            {
                newPrice = new PriceOnliner() {Max = 0, Min = 0};
            }
            if (IsPriceChanged(product, newPrice))
            {
                AddPriceChange(product, newPrice);
                UpdatePrice(product, newPrice);
            }
        }

        private bool IsPriceChanged(Product product, PriceOnliner newPrice)
        {
            if (newPrice == null)
            {
                if (product.MaxPrice != 0 || product.MinPrice != 0)
                {
                    return true;
                }
                return false;
            }
            return newPrice.Min != product.MinPrice || newPrice.Max != product.MaxPrice;
        }

        private void AddPriceChange(Product product, PriceOnliner newPrice)
        {
            var priceChanges = priceChangesMapper.ToDomain(product, newPrice);
            unitOfWork.PriceCanges.Create(priceChanges);
            unitOfWork.Save();
        }

        private void UpdatePrice(Product product, PriceOnliner newPrice)
        {
            product.MaxPrice = newPrice.Max;
            product.MinPrice = newPrice.Min;
            unitOfWork.Save();
        }
    }
}
