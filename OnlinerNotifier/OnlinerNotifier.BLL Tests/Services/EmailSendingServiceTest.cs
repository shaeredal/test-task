using System.Collections.Generic;
using System.Net.Mail;
using Moq;
using NUnit.Framework;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL.Templates.TemplatePathProvider;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.BLL.Wrappers;
using OnlinerNotifier.BLL_Tests.Moq;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL_Tests.Services
{
    [TestFixture]
    public class EmailSendingServiceTest
    {
        private IEmailService emailSendingService;
        private Mock<User> userMock;
        private Mock<ISmtpClient> smtpClientWrapper;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            userMock = mockStorage.UserMock;
            smtpClientWrapper = mockStorage.smtpClientMock;
            var templatePathProviderMock = new Mock<ITemplatePathProvider>();
            templatePathProviderMock.Setup(m => m.GetEmailTemplatePath())
                .Returns(() => "D:\\test-task\\OnlinerNotifier\\OnlinerNotifier.BLL\\Templates\\EmailTemplate.cshtml");
            emailSendingService = new EmailService(new EmailValidator(), smtpClientWrapper.Object, templatePathProviderMock.Object);
        }

        [Test]
        public void SendChanges_EmailSending_SendEmail()
        {
            emailSendingService.SendChanges(userMock.Object, new List<NotificationProductChangesModel>());

            smtpClientWrapper.Verify(m => m.Send(It.IsAny<MailMessage>()), Times.Once);
        }
    }
}
