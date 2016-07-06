using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL.Services
{
    public interface IRedisService
    {
        void PublishEmail(NotificationEmailModel emailModel);
        NotificationEmailModel GetEmail(int key);
    }
}