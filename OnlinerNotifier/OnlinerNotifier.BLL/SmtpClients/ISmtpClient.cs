using System.Net.Mail;

namespace OnlinerNotifier.BLL.Wrappers
{
    public interface ISmtpClient
    {
        void Send(MailMessage message);
    }
}
