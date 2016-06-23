using System.Collections.Generic;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services
{
    public interface IEmailSendingService
    {
        void SendChanges(User user, List<ProductPriceChange> priceChanges);
    }
}
