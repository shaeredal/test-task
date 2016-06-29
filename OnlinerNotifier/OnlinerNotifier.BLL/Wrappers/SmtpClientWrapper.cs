using System.Net;
using System.Net.Mail;

namespace OnlinerNotifier.BLL.Wrappers
{
    public class SmtpClientWrapper : ISmtpClient
    {
        private readonly string address = "OnlinerNotifier@gmail.com";

        private readonly string password = "securepassword";

        private SmtpClient smtpClient;

        public SmtpClientWrapper()
        {
            smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(address, password)
            };
        }

        public void Send(MailMessage message)
        {
            smtpClient.Send(message);
        }
    }
}
