using System;
using System.Collections.Generic;
using FluentScheduler;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.BLL.Services.EmailServices;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.BLL.Services.Interfaces.EmailServices;

namespace OnlinerNotifier.Scheduler.Jobs
{
    public class PushEmailsJob : IJob
    {
        private readonly INotificationDataService _notificationDataService;

        private readonly IEmailBuildingService emailBuilderService;

        private readonly IRedisService redisService;

        public PushEmailsJob(INotificationDataService _notificationDataService, IEmailBuildingService emailBuilderService, IRedisService redisService)
        {
            this._notificationDataService = _notificationDataService;
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
            return _notificationDataService.CollectNotificationData(TimeSpan.FromDays(1));
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