using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers
{
    public class UserProductsMapper
    {
        private ProductMapper productMapper;

        public UserProductsMapper(ProductMapper productMapper)
        {
            this.productMapper = productMapper;
        }

        public UserProduct ToDomain(User user, Product product)
        {
            return new UserProduct()
            {
                User = user,
                Product = product,
                IsTracked = true
            };
        }

        public UserProductViewModel ToModel(UserProduct userProduct)
        {
            return new UserProductViewModel()
            {
                Id = userProduct.Id,
                Product = productMapper.ToModel(userProduct.Product),
                IsTracked = userProduct.IsTracked
            };
        }
    }
}
