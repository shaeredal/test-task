using System.IO;
using System.Linq;
using System.Net;
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

        public PricesCheckingService(UnitOfWork unitOfWork, PriceChangesMapper priceChangesMapper)
        {
            this.unitOfWork = unitOfWork;
            this.priceChangesMapper = priceChangesMapper;
        }

        public void Check()
        {
            var products = unitOfWork.Products.GetAll().ToList();
            foreach (var product in products)
            {
                string searchResultString = OnlinerSearch(product);
                var newProduct = ParseProduct(searchResultString, product.OnlinerId);
                if (newProduct != null)
                {
                    CompareAndUpdate(product, newProduct);
                }               
            }
        }

        private string OnlinerSearch(Product product)
        {
            var requestString = $"https://catalog.api.onliner.by/search/products?query={product.Name}";
            var request = (HttpWebRequest)WebRequest.Create(requestString);
            request.Method = "GET";
            request.Accept = "application/json";
            var response = (HttpWebResponse)request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
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
