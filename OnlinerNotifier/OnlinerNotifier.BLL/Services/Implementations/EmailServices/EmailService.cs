using System;
using System.Net.Mail;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services.EmailServices;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.BLL.Wrappers;

namespace OnlinerNotifier.BLL.Services.Implementations.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly string address = "OnlinerNotifier@gmail.com";

        private EmailValidator emailValidator;

        private ISmtpClient smtpClient;

        public EmailService(EmailValidator emailValidator, ISmtpClient smtpClient)
        {
            this.smtpClient = smtpClient;
            this.emailValidator = emailValidator;
        }

        public void Send(NotificationEmailModel emailModel)
        {
            var email = emailModel.EmailAddress;
            if (!emailValidator.IsValid(email))
            {
                throw new Exception("Email address is not valid.");
            }
            var toAddress = new MailAddress(email, emailModel.ReceiverName);
            Send(toAddress, emailModel.EmailBody, emailModel.Subject, true);
        }

        public void Send(MailAddress toAddress, string mailBody, string subject, bool isHtml)
        {
            var fromAddress = new MailAddress(address);
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = mailBody,
                IsBodyHtml = isHtml
            })
            {
                smtpClient.Send(message);
            }
        }
    }
}
