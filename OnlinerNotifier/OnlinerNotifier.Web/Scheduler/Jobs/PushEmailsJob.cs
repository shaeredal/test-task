using System.Collections.Generic;
using System.Web.Hosting;
using FluentScheduler;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services;

namespace OnlinerNotifier.Scheduler.Jobs
{
    public class PushEmailsJob : IJob
    {
        private readonly INotificationService notificationService;

        private readonly IEmailService emailService;

        private readonly IRedisService redisService;

        public PushEmailsJob(INotificationService notificationService, IEmailService emailService, IRedisService redisService)
        {
            this.notificationService = notificationService;
            this.emailService = emailService;
            this.redisService = redisService;
        }

        public void Execute()
        {
            var data = GetData();
            PublishEmails(data);
        }

        private List<NotificationDataModel> GetData()
        {
            return notificationService.GetNotificationData();
        }

        private void PublishEmails(List<NotificationDataModel> data)
        {
            foreach (var userData in data)
            {
                var email = emailService.GetEmail(userData.User, userData.Products);
                redisService.PublishEmail(email);
            }
        }
    }
}