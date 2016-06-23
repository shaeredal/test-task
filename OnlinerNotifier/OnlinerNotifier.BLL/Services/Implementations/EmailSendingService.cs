using System.Collections.Generic;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class EmailSendingService : IEmailSendingService
    {
        public void SendChanges(User user, List<ProductPriceChange> priceChanges)
        {
            //TODO: Send email
        }
    }
}
