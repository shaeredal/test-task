using System.Net.Mail;
using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL.Services.Interfaces.EmailServices
{
    public interface IEmailService
    {
        void Send(NotificationEmailModel emailModel);

        void Send(MailAddress toAddress, string mailBody, string subject, bool isHtml);
    }
}
