using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class EmailSendingService : IEmailSendingService
    {
        public void SendChanges(User user, List<NotificationProductChangesModel> priceChanges)
        {
            var email = user.Email;
            if (email == null)
            {
                return;
            }
            var fromAddress = new MailAddress("OnlinerNotifier@gmail.com", "Onliner Notifier");
            var toAddress = new MailAddress(email, $"{user.FirstName} {user.LastName}");
            const string fromPassword = "securepassword";
            const string subject = "Price Changes";
            string body = GetMailBody(priceChanges);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                //Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        private string GetMailBody(List<NotificationProductChangesModel> priceChanges)
        {
            //TODO: create email body
            return "Body";
        }
    }
}
