using OnlinerNotifier.BLL.Models.TrackingModels;

namespace OnlinerNotifier.BLL.Services
{
    public interface ITrackingService
    {
        bool ChangeStatus(TrackingModel trackingModel);
    }
}
