using System.Linq;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Mappers.Implementations;
using OnlinerNotifier.BLL.Models.OnlinerDataModels;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class PricesCheckingService : IPricesCheckingService
    {
        private IUnitOfWork unitOfWork;

        private PriceChangesMapper priceChangesMapper;

        private IOnlinerSearchService onlinerSearchService;

        public PricesCheckingService(IUnitOfWork unitOfWork, PriceChangesMapper priceChangesMapper, IOnlinerSearchService onlinerSearchService)
        {
            this.unitOfWork = unitOfWork;
            this.priceChangesMapper = priceChangesMapper;
            this.onlinerSearchService = onlinerSearchService;
        }

        public void Check()
        {
            var products = unitOfWork.Products.GetAll().ToList();
            foreach (var product in products)
            {
                var searchResult = onlinerSearchService.Search(product.Name);
                var newProduct = FindProduct(searchResult, product.OnlinerId);
                if (newProduct != null)
                {
                    CompareAndUpdate(product, newProduct);
                }               
            }
        }

        private ProductOnliner FindProduct(SearchResultOnliner searchResult, int id)
        {
            return searchResult.Products.FirstOrDefault(prod => prod.Id == id);
        }

        private void CompareAndUpdate(Product product, ProductOnliner newProduct)
        {
            if (newProduct.Prices == null)
            {
                newProduct.Prices = new PriceOnliner()
                {
                    Min = 0,
                    Max = 0
                };
            }

            if (newProduct.Prices.Min != product.MinPrice || newProduct.Prices.Max != product.MaxPrice)
            {
                AddPriceChange(product, newProduct);
                UpdateProduct(product, newProduct);
            }
        }

        private void AddPriceChange(Product product, ProductOnliner newProduct)
        {
            var priceChanges = priceChangesMapper.ToDomain(product, newProduct);
            unitOfWork.PriceCanges.Create(priceChanges);
            unitOfWork.Save();
        }

        private void UpdateProduct(Product product, ProductOnliner newProduct)
        {
            product.MaxPrice = newProduct.Prices.Max;
            product.MinPrice = newProduct.Prices.Min;
            unitOfWork.Save();
        }
    }
}
