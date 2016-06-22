using System.Collections.Generic;
using System.Linq;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class ProductService : IProductService
    {
        private UnitOfWork unitOfWork;
        private ProductMapper productMapper;

        public ProductService(UnitOfWork unitOfWork, ProductMapper productMapper)
        {
            this.unitOfWork = unitOfWork;
            this.productMapper = productMapper;
        }

        public void Add(ProductViewModel productModel, int userId)
        {
            var products = unitOfWork.Products;

            var product = products.GetAll().FirstOrDefault(prod => prod.OnlinerId == productModel.OnlinerId);

            if (product == null)
            {
                product = productMapper.ToDomain(productModel);
                products.Create(product);
                unitOfWork.Save();
            }

            AddToUser(product, userId);
        }

        private void AddToUser(Product product, int userId)
        {
            var user = unitOfWork.Users.Get(userId);
            if (!user.Products.Contains(product))
            {
                user.Products.Add(product);
                unitOfWork.Save();
            }
        }

        public List<ProductViewModel> GetUserProducts(int userId)
        {
            var user = unitOfWork.Users.Get(userId);
            return user.Products.Select(prod => productMapper.ToModel(prod)).ToList();
        }

        public bool Delete(int userId, int productId)
        {
            var user = unitOfWork.Users.Get(userId);
            var product = unitOfWork.Products.Get(productId);
            if (user.Products.Contains(product))
            {
                user.Products.Remove(product);
                unitOfWork.Save();
                if (!product.Users.Any())
                {
                    if (unitOfWork.Products.Get(productId) == null)
                    {
                        return false;
                    }
                    unitOfWork.Products.Delete(productId);
                    unitOfWork.Save();
                }
                return true;
            }
            return false;
        }
    }
}
