using OnlinerNotifier.DAL;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class PricesCheckingService : IPricesCheckingService
    {
        private UnitOfWork unitOfWork;

        public PricesCheckingService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Check()
        {
            //checking prices chaning
        } 
    }
}
