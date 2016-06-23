using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers
{
    public class UserProductsMapper
    {
        public UserProduct ToDomain(User user, Product product)
        {
            return new UserProduct()
            {
                User = user,
                Product = product,
                IsTracked = true
            };
        }
    }
}
