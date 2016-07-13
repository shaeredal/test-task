using System.Net.Mail;

namespace OnlinerNotifier.BLL.SmtpClients
{
    public class SmtpClientWrapper : ISmtpClient
    {
        private SmtpClient smtpClient;

        public SmtpClientWrapper()
        {
            smtpClient = new SmtpClient();
        }

        public void Send(MailMessage message)
        {
            smtpClient.Send(message);
        }
    }
}
