using System.Net.Mail;

namespace OnlinerNotifier.BLL.Providers
{
    public interface ISmtpClientProvider
    {
        SmtpClient GetGmailSmtpClient(string address, string password);
    }
}
