using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL.Services.EmailServices
{
    public interface IEmailModelSender
    {
        void Send(NotificationEmailModel emailModel);
    }
}
