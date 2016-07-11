using System.Collections.Generic;
using FluentScheduler;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.BLL.Services.Interfaces.EmailServices;

namespace OnlinerNotifier.Scheduler.Jobs
{
    public class PushEmailsJob : IJob
    {
        private readonly INotificationService notificationService;

        private readonly IEmailBuildingService emailBuilderService;

        private readonly IRedisService redisService;

        public PushEmailsJob(INotificationService notificationService, IEmailBuildingService emailBuilderService, IRedisService redisService)
        {
            this.notificationService = notificationService;
            this.emailBuilderService = emailBuilderService;
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
                var email = emailBuilderService.GetEmail(userData.User, userData.Products);
                redisService.PublishEmail(email);
            }
        }
    }
}