using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Templates.TemplatePathProvider;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.BLL.Wrappers;
using OnlinerNotifier.DAL.Models;
using RazorEngine;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private EmailValidator emailValidator;

        private readonly string address = "OnlinerNotifier@gmail.com";

        private readonly string senderName = "Onliner Notifier";

        private ISmtpClient smtpClient;

        private ITemplatePathProvider templatePathProvider;

        private EmailMapper emailMapper;

        public EmailService(EmailValidator emailValidator, 
            ISmtpClient smtpClient, 
            ITemplatePathProvider templatePathProvider,
            EmailMapper emailMapper)
        {
            this.emailValidator = emailValidator;          
            this.smtpClient = smtpClient;
            this.templatePathProvider = templatePathProvider;
            this.emailMapper = emailMapper;
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

        public void SendChanges(NotificationEmailModel emailModel)
        {
            var email = emailModel.EmailAddress;
            if (!emailValidator.IsValid(email))
            {
                throw new Exception("Email address is not valid.");
            }
            var fromAddress = new MailAddress(address, senderName);
            var toAddress = new MailAddress(email, emailModel.ReceiverName);
            string subject = "Price Changes";
            string body = emailModel.EmailBody;
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

        public NotificationEmailModel GetEmail(User user, List<NotificationProductChangesModel> priceChanges)
        {
            return emailMapper.ToModel(user, GetMailBody(priceChanges));
        }
    }
}
