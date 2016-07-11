using System;
using OnlinerNotifier.BLL.Models.OnlinerDataModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Implementations
{
    public class PriceChangesMapper
    {
        public ProductPriceChange ToDomain(Product productOld, ProductOnliner productNew)
        {
            return new ProductPriceChange()
            {
                CheckTime = DateTime.Now,
                Product = productOld,
                OldMinPrice = productOld.MinPrice,
                NewMinPrice = productNew.Prices.Min,
                OldMaxPrice = productOld.MaxPrice,
                NewMaxPrice = productNew.Prices.Max
            };
        }
    }
}
