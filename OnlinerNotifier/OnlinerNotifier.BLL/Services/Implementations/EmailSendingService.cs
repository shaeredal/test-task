using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Templates.TemplatePathProvider;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.BLL.Wrappers;
using OnlinerNotifier.DAL.Models;
using RazorEngine;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class EmailSendingService : IEmailSendingService
    {
        private EmailValidator emailValidator;

        private readonly string address = "OnlinerNotifier@gmail.com";

        private readonly string senderName = "Onliner Notifier";

        private ISmtpClient smtpClient;

        private ITemplatePathProvider templatePathProvider;

        public EmailSendingService(EmailValidator emailValidator, ISmtpClient smtpClient, ITemplatePathProvider templatePathProvider)
        {
            this.emailValidator = emailValidator;          
            this.smtpClient = smtpClient;
            this.templatePathProvider = templatePathProvider;
        }

        public void SendChanges(User user, List<NotificationProductChangesModel> priceChanges)
        {
            var email = user.Email;
            if (!emailValidator.IsValid(email))
            { 
                throw new Exception("Email address is not valid.");
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
            var templatePath = templatePathProvider.GetEmailTemplatePath();
            var template = File.ReadAllText(templatePath);
            return Razor.Parse(template, priceChanges);
        }
    }
}
