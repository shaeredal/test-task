using System.Linq;
using Newtonsoft.Json;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Models.OnlinerDataModels;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class PricesCheckingService : IPricesCheckingService
    {
        private UnitOfWork unitOfWork;

        private PriceChangesMapper priceChangesMapper;

        private IOnlinerSearchService onlinerSearchService;

        public PricesCheckingService(UnitOfWork unitOfWork, PriceChangesMapper priceChangesMapper, IOnlinerSearchService onlinerSearchService)
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
                string searchResultString = onlinerSearchService.Search(product.Name);
                var newProduct = ParseProduct(searchResultString, product.OnlinerId);
                if (newProduct != null)
                {
                    CompareAndUpdate(product, newProduct);
                }               
            }
        }

        private ProductOnliner ParseProduct(string searchResultString, int id)
        {
            var searchResult = JsonConvert.DeserializeObject<SearchResultOnliner>(searchResultString);
            return searchResult.Products.FirstOrDefault(prod => prod.Id == id);
        }

        private void CompareAndUpdate(Product product, ProductOnliner newProduct)
        {
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
