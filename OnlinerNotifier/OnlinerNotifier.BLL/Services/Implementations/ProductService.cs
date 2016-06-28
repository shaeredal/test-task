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
        private IUnitOfWork unitOfWork;
        private ProductMapper productMapper;
        private UserProductsMapper userProductsMapper;

        public ProductService(IUnitOfWork unitOfWork, ProductMapper productMapper, UserProductsMapper userProductsMapper)
        {
            this.unitOfWork = unitOfWork;
            this.productMapper = productMapper;
            this.userProductsMapper = userProductsMapper;
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
            if (!user.UserProducts.Select(up => up.Product).Contains(product))
            {
                var userProduct = userProductsMapper.ToDomain(user, product);
                user.UserProducts.Add(userProduct);
                unitOfWork.Save();
            }
        }

        public List<ProductViewModel> GetUserProducts(int userId)
        {
            var user = unitOfWork.Users.Get(userId);
            return user.UserProducts.Select(up => up.Product).Select(prod => productMapper.ToModel(prod)).ToList();
        }

        public bool Delete(int userId, int productId)
        {
            var user = unitOfWork.Users.Get(userId);
            var product = unitOfWork.Products.Get(productId);
            if (user.UserProducts.Select(up => up.Product).Contains(product))
            {
                var userProduct = unitOfWork.UserProducts.GetAll().First(x => x.User == user && x.Product == product);
                unitOfWork.UserProducts.Delete(userProduct.Id);
                unitOfWork.Save();
                if (!product.UserProducts.Any())
                {
                    if (!unitOfWork.Products.Delete(productId))
                    {
                        return false;
                    }
                    unitOfWork.Save();
                }
                return true;
            }
            return false;
        }
    }
}
