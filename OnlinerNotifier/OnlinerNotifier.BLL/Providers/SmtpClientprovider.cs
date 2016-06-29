using System.Net;
using System.Net.Mail;

namespace OnlinerNotifier.BLL.Providers
{
    public class SmtpClientProvider : ISmtpClientProvider
    {
        public SmtpClient GetGmailSmtpClient(string address, string password)
        {
            return new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(address, password),
            };
        }
    }
}
