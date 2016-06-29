using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Providers;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.DAL.Models;
using RazorEngine;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class EmailSendingService : IEmailSendingService
    {
        private EmailValidator emailValidator;

        private SmtpClient smtpClient;

        private readonly string address = "OnlinerNotifier@gmail.com";

        private readonly string senderName = "Onliner Notifier";

        private readonly string password = "securepassword";

        public EmailSendingService(EmailValidator emailValidator, ISmtpClientProvider smtpClientProvider)
        {
            this.emailValidator = emailValidator;
            this.smtpClient = smtpClientProvider.GetGmailSmtpClient(address, password);
        }

        public void SendChanges(User user, List<NotificationProductChangesModel> priceChanges)
        {
            var email = user.Email;
            if (!emailValidator.IsValid(email))
            { 
                throw new Exception("Email is invalid.");
            }
            var fromAddress = new MailAddress(address, senderName);
            var toAddress = new MailAddress(email, $"{user.FirstName} {user.LastName}");
            string subject = "Price Changes";
            string body = GetMailBody(priceChanges);
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtpClient.Send(message);
            }
        }

        private string GetMailBody(List<NotificationProductChangesModel> priceChanges)
        {
            var templatePath = Path.Combine(HttpRuntime.AppDomainAppPath,
                "..\\OnlinerNotifier.BLL\\Templates\\EmailTemplate.cshtml");
            var template = File.ReadAllText(templatePath);
            return Razor.Parse(template, priceChanges);
        }
    }
}
